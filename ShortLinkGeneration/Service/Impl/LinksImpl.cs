using System.Text.RegularExpressions;
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

public class LinksImpl : ILinksService
{
    private readonly ILogger<LinksImpl> _logger;
    private readonly ShortLinkContext _db;
    private readonly IJwtService _jwtService;
    private readonly IOptions<ConfigOptions> _config;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public LinksImpl(ILogger<LinksImpl> logger, ShortLinkContext db, IJwtService jwtService,
        IOptions<ConfigOptions> config, IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _db = db;
        _jwtService = jwtService;
        _config = config;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IRe<LinksResponse.CreateResponse>> Create(LinksRequest.CreateRequest data)
    {
        //从Headers中获取Token
        string token = _httpContextAccessor.HttpContext!.Request.Headers["Authorization"]!.ToString().Split(' ').Last();

        if (data.ShortLink is not null && data.ShortLink.Length != 0 && JudgeShortConnection(data.ShortLink))
        {
            return new Ok<LinksResponse.CreateResponse>
            {
                Code = Code.ShortLinkExists,
                Message = "短连接已存在"
            };
        }

        //Token不存在，直接生成短链接
        if (token == "")
        {
            var shortLink = CreateShortLink(data.ShortLink, data.LongLink, data.ExpiryDate, data.MaxClicks);
            //将短链接存入数据库
            _db.Links.Add(new Link()
            {
                ShortLink = shortLink,
                OriginalLink = data.LongLink,
                UserID = null,
                CreationDate = DateTime.Now,
                ClickCount = 0,
                ExpiryDate = data.ExpiryDate,
                MaxClicks = data.MaxClicks,
                IsDisabled = false
            });

            //保存数据库
            await _db.SaveChangesAsync();

            return new Ok<LinksResponse.CreateResponse>
            {
                Code = Code.Success,
                Message = "短链接生成成功",
                Data = new LinksResponse.CreateResponse
                {
                    ShortLink = shortLink
                }
            };
        }
        else
        {
            //Token存在，判断Token是否有效
            var isValid = _jwtService.ValidateTokenAsync(token, "Admin").Result;
            //判断令牌是否在缓存中
            isValid = isValid && TokenList.TokenLists.Any(x => x.Token == token);

            if (!isValid)
            {
                return new Error<LinksResponse.CreateResponse>
                {
                    Code = Code.TokenError,
                    Message = "Token错误"
                };
            }

            //根据用户名查找用户id
            var userID = _db.Users
                .FirstOrDefault(x => x.Username == _jwtService.GetUsernameAsync(token).Result.ToString())?.UserID;

            if (userID is null)
            {
                return new Error<LinksResponse.CreateResponse>
                {
                    Code = Code.UsernameNotExist,
                    Message = "用户不存在"
                };
            }

            //生成短链接
            var shortLink = CreateShortLink(data.ShortLink, data.LongLink, data.ExpiryDate, data.MaxClicks);
            //将短链接存入数据库
            _db.Links.Add(new Link()
            {
                ShortLink = shortLink,
                OriginalLink = data.LongLink,
                UserID = userID,
                CreationDate = DateTime.Now,
                ClickCount = 0,
                ExpiryDate = data.ExpiryDate,
                MaxClicks = data.MaxClicks,
                IsDisabled = false
            });

            //保存数据库
            await _db.SaveChangesAsync();

            return new Ok<LinksResponse.CreateResponse>
            {
                Code = Code.Success,
                Message = "短链接生成成功",
                Data = new LinksResponse.CreateResponse
                {
                    ShortLink = shortLink
                }
            };
        }
    }

    /// <summary>
    /// 游客生成短连接
    /// </summary>
    /// <param name="shortLink"></param>
    /// <param name="longLink"></param>
    /// <param name="expiryDate"></param>
    /// <returns></returns>
    private string CreateShortLink(string? shortLink, string longLink, DateTime? expiryDate, int? maxClicks)
    {
        string tempShortLink = shortLink ?? "";
        //生成短链接
        if (shortLink is null || shortLink.Length == 0)
        {
            tempShortLink = RandomlyGenerateShortLinks();
        }

        //判断短链接是否存在
        if (JudgeShortConnection(tempShortLink))
        {
            CreateShortLink(shortLink, longLink, expiryDate, maxClicks);
        }

        shortLink = tempShortLink;

        return shortLink;
    }

    /// <summary>
    /// 随机生成短连接
    /// </summary>
    /// <returns></returns>
    public string RandomlyGenerateShortLinks()
    {
        // 可以使用的字符
        char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();

        // 创建一个随机对象
        Random random = new Random();

        // 初始化结果字符串
        string shortLink = string.Empty;

        // 循环5次，每次随机选择一个字符加入结果字符串
        for (int i = 0; i < 5; i++)
        {
            shortLink += chars[random.Next(chars.Length)];
        }

        return shortLink;
    }


    /// <summary>
    /// 判断短连接是否存在
    /// </summary>
    /// <param name="shortLink"></param>
    /// <returns></returns>
    private bool JudgeShortConnection(string shortLink)
    {
        if (!DetermineTheShortConnectionFormat(shortLink))
        {
            return false;
        }

        return _db.Links.Any(x => x.ShortLink == shortLink);
    }

    /// <summary>
    /// 判断短连接格式
    /// </summary>
    /// <param name="shortLink"></param>
    /// <returns></returns>
    private bool DetermineTheShortConnectionFormat(string shortLink)
    {
        if (string.IsNullOrEmpty(shortLink))
            return false;

        // 使用正则表达式判断格式是否正确
        string pattern = @"^[a-z0-9_-]+$";
        Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

        return regex.IsMatch(shortLink);
    }
}