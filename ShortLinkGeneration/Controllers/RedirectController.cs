using Microsoft.AspNetCore.Mvc;
using ShortLinkGeneration.Entity;
using ShortLinkGeneration.Entity.Enum;
using ShortLinkGeneration.Entity.Request;
using ShortLinkGeneration.Entity.Response;
using ShortLinkGeneration.Service.Service;

namespace ShortLinkGeneration.Controllers;

/// <summary>
/// 重定向控制器
/// </summary>
[ApiController]
[Route("/")]
public class RedirectController
{
    private readonly IRedirectService _redirectService;

    public RedirectController(IRedirectService redirectService)
    {
        _redirectService = redirectService;
    }

    /// <summary>
    /// 链接重定向
    /// </summary>
    /// <param name="shortLink"></param>
    /// <returns></returns>
    [HttpGet("{shortLink}")]
    public async Task<IActionResult> RedirectGet(string shortLink)
    {
        return await _redirectService.Redirect(shortLink);
    }

    /// <summary>
    /// 连接重定向
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [HttpPost("api/redirect")]
    public async Task<IRe<RedirectResponse.RedirectPostResponse>> RedirectPost(
        RedirectRequest.RedirectPostRequest data)
    {
        return await _redirectService.RedirectPost(data);
    }
}