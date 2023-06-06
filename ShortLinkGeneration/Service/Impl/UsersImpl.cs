using Microsoft.Extensions.Options;
using ShortLinkGeneration.DB;
using ShortLinkGeneration.Entity;
using ShortLinkGeneration.Entity.Enum;
using ShortLinkGeneration.Entity.Request;
using ShortLinkGeneration.Entity.Response;
using ShortLinkGeneration.Service.Service;
using ShortLinkGeneration.Static;
using ShortLinkGeneration.Tool;

namespace ShortLinkGeneration.Service.Impl;

public class UsersImpl : IUsersService
{
    private readonly ILogger<UsersImpl> _logger;
    private readonly ShortLinkContext _db;
    private readonly IJwtService _jwtService;
    private readonly IOptions<ConfigOptions> _config;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UsersImpl(
        ILogger<UsersImpl> logger,
        ShortLinkContext db,
        IJwtService jwtService,
        IOptions<ConfigOptions> config,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _logger = logger;
        _db = db;
        _jwtService = jwtService;
        _config = config;
        _httpContextAccessor = httpContextAccessor;
    }

    public IRe<UsersResponse.RegisterResponse> Register(UsersRequest.RegisterRequest data)
    {
        //判断账号密码格式是否正确
        if (!data.Username.IsEmail() || !data.Password.IsPassword())
        {
            return new Error<UsersResponse.RegisterResponse>
            {
                Code = Code.UsernameOrPasswordFormatError,
                Message = "账号或密码格式错误"
            };
        }

        //判断账号是否存在
        var user = _db.Users.FirstOrDefault(u => u.Username == data.Username);
        if (user != null)
        {
            return new Error<UsersResponse.RegisterResponse>
            {
                Code = Code.UsernameExist,
                Message = "账号已存在"
            };
        }

        //判断验证码是否正确
        bool isCodeOk = VerificationCode.VerificationCodeList.Any(item =>
            data.Code == item.Code && item.ExpireTime > DateTime.Now && item.Email == data.Username);

        if (!isCodeOk)
        {
            return new Error<UsersResponse.RegisterResponse>
            {
                Code = Code.CodeError,
            };
        }

        //创建管理员账户
        var newUser = new User
        {
            Username = data.Username,
            PasswordHash = data.Password.HashPassword(data.Username),
            Role = Role.User,
            CreationTime = DateTime.Now,
        };
        _db.Users.Add(newUser);
        _db.SaveChanges();

        //生成Token
        string token = _jwtService.CreateTokenAsync(newUser.Username, newUser.Role).Result;

        return new Ok<UsersResponse.RegisterResponse>
        {
            Code = Code.Success,
            Message = "用户注册成功",
            Data = new UsersResponse.RegisterResponse
            {
                Token = token,
                Username = newUser.Username,
                Role = newUser.Role,
            }
        };
    }

    public IRe<UsersResponse.LoginResponse> Login(UsersRequest.LoginRequest data)
    {
        //判断账号密码格式是否正确
        if (!data.Username.IsEmail() || !data.Password.IsPassword())
        {
            return new Error<UsersResponse.LoginResponse>
            {
                Code = Code.UsernameOrPasswordFormatError,
                Message = "账号或密码格式错误"
            };
        }

        //判断账号是否存在
        var user = _db.Users.FirstOrDefault(u => u.Username == data.Username);
        if (user == null)
        {
            return new Error<UsersResponse.LoginResponse>
            {
                Code = Code.UsernameNotExist,
                Message = "账号不存在"
            };
        }

        //判断密码是否正确
        if (!data.Password.VerifyPassword(user.PasswordHash, user.Username))
        {
            return new Error<UsersResponse.LoginResponse>
            {
                Code = Code.PasswordError,
                Message = "密码错误"
            };
        }

        //生成token
        string token = _jwtService.CreateTokenAsync(user.Username, user.Role).Result;
        return new Ok<UsersResponse.LoginResponse>
        {
            Code = Code.Success,
            Message = "登录成功",
            Data = new UsersResponse.LoginResponse()
            {
                Username = user.Username,
                Role = user.Role,
                Token = token
            }
        };
    }

    public IRe<UsersResponse.SendCodeResponse> SendCode(UsersRequest.SendCodeRequest data)
    {
        //判断邮箱格式是否正确
        if (!data.Username.IsEmail())
        {
            return new Error<UsersResponse.SendCodeResponse>
            {
                Code = Code.EmailFormatError,
                Message = "邮箱格式错误"
            };
        }

        //生成验证码
        var code = new Random().Next(100000, 999999);
        //将验证码保存到内存
        VerificationCode.VerificationCodeList.Add(new()
        {
            Code = code,
            ExpireTime = DateTime.Now.AddMinutes(10),
            Email = data.Username
        });

        string emailBody = TemplateReplacer.ReplacePlaceholders(new Dictionary<string, string>()
        {
            { "Subject", "ShortLink验证码" },
            { "imageUrl", "https://img-cdn.ccrui.cn/2023/04/18/v2-0768f34e8784f31dbbf1be10118e33a6_r.jpg" },
            { "projectName", "ShortLink" },
            { "UserName", "用户" },
            { "content", $"您的验证码为{code}，请在10分钟内使用" },
            { "information", "如有任何疑问或需要进一步的信息，请随时联系我们。<br><br>期待为您提供更多的帮助！" },
            { "footnote", "© 2023 All rights reserved" }
        });

        //发送验证码到邮箱
        SMTPMail smtpMail = new SMTPMail(new MailConfiguration()
        {
            smtpService = _config.Value.SmtpService,
            sendEmail = _config.Value.SendEmail,
            sendPwd = _config.Value.SendPwd,
            port = _config.Value.Port,
            reAddress = data.Username,
            subject = "验证码",
            body = emailBody
        });

        if (smtpMail.Send())
        {
            return new Ok<UsersResponse.SendCodeResponse>()
            {
                Code = Code.Success,
                Message = "发送成功"
            };
        }
        else
        {
            return new Error<UsersResponse.SendCodeResponse>()
            {
                Code = Code.SendCodeError,
                Message = "发送失败"
            };
        }
    }

    public IRe<UsersResponse.InfoResponse> Info(UsersRequest.InfoRequest data)
    {
        //从Headers中获取Token
        string token = _httpContextAccessor.HttpContext!.Request.Headers["Authorization"]!.ToString().Split(' ').Last();
        //从Token中获取用户名
        string username = _jwtService.GetUsernameAsync(token).Result;
        if (username == "")
        {
            return new Error<UsersResponse.InfoResponse>
            {
                Code = Code.TokenError,
                Message = "Token错误"
            };
        }

        //从数据库中获取用户信息
        var user = _db.Users.FirstOrDefault(u => u.Username == username);
        if (user == null)
        {
            return new Error<UsersResponse.InfoResponse>
            {
                Code = Code.UsernameNotExist,
                Message = "账号不存在"
            };
        }

        //从数据库中获取用户创建的短连接数量
        int shortLinkCount = _db.Links.Count(l => l.UserID == user.UserID);

        return new Ok<UsersResponse.InfoResponse>
        {
            Code = Code.Success,
            Message = "获取成功",
            Data = new UsersResponse.InfoResponse
            {
                Username = user.Username,
                Role = user.Role,
                CreationTime = user.CreationTime,
                LinkCount = shortLinkCount
            }
        };
    }
}