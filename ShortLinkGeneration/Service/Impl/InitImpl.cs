using Microsoft.EntityFrameworkCore;
using ShortLinkGeneration.Controllers;
using ShortLinkGeneration.DB;
using ShortLinkGeneration.Entity;
using ShortLinkGeneration.Entity.Response;
using ShortLinkGeneration.Service.Service;

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

    public IRe<InitResponse.InitDb> InitDb()
    {
        //判断数据库连接是否成功
        if (_db.Database.CanConnect())
        {
            //判断是否存在未执行的迁移
            if (_db.Database.GetPendingMigrations().Any())
            {
                //如果存在则执行迁移
                _db.Database.Migrate();
                _logger.LogInformation("数据库初始化成功");
                return new Ok<InitResponse.InitDb>
                {
                    Message = "数据库初始化成功"
                };
            }

            _logger.LogInformation("数据库连接成功");
            return new Ok<InitResponse.InitDb>
            {
                Message = "数据库连接成功"
            };
        }
        else
        {
            _logger.LogError("数据库连接失败");
            return new Error<InitResponse.InitDb>
            {
                Code = Code.ConnectionFailedDb,
                Message = "数据库连接失败"
            };
        }
    }
}