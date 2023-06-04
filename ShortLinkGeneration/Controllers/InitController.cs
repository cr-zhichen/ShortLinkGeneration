using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShortLinkGeneration.DB;
using ShortLinkGeneration.Entity;
using ShortLinkGeneration.Entity.Response;
using ShortLinkGeneration.Service.Service;

namespace ShortLinkGeneration.Controllers;

/// <summary>
/// 初始化控制器
/// </summary>
[ApiController]
// [Route("[controller]")]
[Route("init")]
public class InitController : ControllerBase
{
    readonly IInitService _initService;

    public InitController(IInitService initService)
    {
        _initService = initService;
    }

    /// <summary>
    /// 初始化数据库连接
    /// </summary>
    /// <returns></returns>
    [HttpPost("db")]
    public IRe<InitResponse.InitDb> InitDb()
    {
        return _initService.InitDb();
    }
}