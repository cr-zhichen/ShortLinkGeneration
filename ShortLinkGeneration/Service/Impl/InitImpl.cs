using Microsoft.EntityFrameworkCore;
using ShortLinkGeneration.Controllers;
using ShortLinkGeneration.DB;
using ShortLinkGeneration.Entity;
using ShortLinkGeneration.Entity.Enum;
using ShortLinkGeneration.Entity.Request;
using ShortLinkGeneration.Entity.Response;
using ShortLinkGeneration.Service.Service;
using ShortLinkGeneration.Tool;

namespace ShortLinkGeneration.Service.Impl;

public class InitImpl : IInitService
{
    private readonly ILogger<InitImpl> _logger;
    private readonly ShortLinkContext _db;

    public InitImpl(ILogger<InitImpl> logger, ShortLinkContext db)
    {
        _logger = logger;
        _db = db;
    }

    public async Task<IRe<InitResponse.InitDbResponse>> InitDb()
    {
        //判断数据库连接是否成功
        if (await _db.Database.CanConnectAsync())
        {
            //判断是否存在未执行的迁移
            if ((await _db.Database.GetPendingMigrationsAsync()).Any())
            {
                //如果存在则执行迁移
                await _db.Database.MigrateAsync();
                _logger.LogInformation("数据库初始化成功");
                return new Ok<InitResponse.InitDbResponse>
                {
                    Message = "数据库初始化成功"
                };
            }

            _logger.LogInformation("数据库连接成功");
            return new Ok<InitResponse.InitDbResponse>
            {
                Message = "数据库连接成功"
            };
        }
        else
        {
            _logger.LogError("数据库连接失败");
            return new Error<InitResponse.InitDbResponse>
            {
                Code = Code.ConnectionFailedDb,
                Message = "数据库连接失败"
            };
        }
    }

    public async Task<IRe<InitResponse.InitAdminResponse>> InitAdmin(
        InitRequest.InitAdminRequest data)
    {
        //判断是否存在管理员账户
        if (_db.Users.Any(x => x.Role == Role.Admin))
        {
            _logger.LogInformation("管理员账户已存在");
            return new Error<InitResponse.InitAdminResponse>
            {
                Code = Code.AdminAccountAlreadyExists,
                Message = "管理员账户已存在"
            };
        }

        if (!data.Username.IsEmail() || !data.Password.IsPassword())
        {
            return new Error<InitResponse.InitAdminResponse>
            {
                Code = Code.UsernameOrPasswordFormatError,
                Message = "账号或密码格式错误"
            };
        }

        //创建管理员账户
        var user = new User
        {
            Username = data.Username,
            PasswordHash = data.Password.HashPassword(data.Username),
            Role = Role.Admin,
            CreationTime = DateTime.Now,
        };
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        _logger.LogInformation("管理员账户创建成功");
        return new Ok<InitResponse.InitAdminResponse>
        {
            Message = "管理员账户创建成功"
        };
    }
}