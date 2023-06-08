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
    public async Task<IRe<InitResponse.InitDbResponse>> InitDb()
    {
        return await _initService.InitDb();
    }

    /// <summary>
    /// 判断初始化完成
    /// </summary>
    /// <returns></returns>
    [HttpPost("is-init")]
    public async Task<IRe<InitResponse.IsInitResponse>> IsInit()
    {
        return await _initService.IsInit();
    }

    /// <summary>
    /// 设定管理员账户
    /// </summary>
    /// <returns></returns>
    [HttpPost("admin")]
    public async Task<IRe<InitResponse.InitAdminResponse>> InitAdmin(
        InitRequest.InitAdminRequest data)
    {
        return await _initService.InitAdmin(data);
    }
}