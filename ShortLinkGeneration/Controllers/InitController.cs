using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShortLinkGeneration.DB;
using ShortLinkGeneration.Entity;
using ShortLinkGeneration.Entity.Request;
using ShortLinkGeneration.Entity.Response;
using ShortLinkGeneration.Service.Service;

namespace ShortLinkGeneration.Controllers;

/// <summary>
/// 初始化控制器
/// </summary>
[ApiController]
// [Route("[controller]")]
[Route("/api/init")]
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
    public IRe<InitResponse.InitDbResponse> InitDb()
    {
        return _initService.InitDb();
    }

    /// <summary>
    /// 设定管理员账户
    /// </summary>
    /// <returns></returns>
    [HttpPost("admin")]
    public IRe<InitResponse.InitAdminResponse> InitAdmin(
        InitRequest.InitAdminRequest data)
    {
        return _initService.InitAdmin(data);
    }
}