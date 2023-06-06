using Microsoft.AspNetCore.Mvc;
using ShortLinkGeneration.Attribute;
using ShortLinkGeneration.Entity;
using ShortLinkGeneration.Entity.Request;
using ShortLinkGeneration.Entity.Response;
using ShortLinkGeneration.Service.Service;

namespace ShortLinkGeneration.Controllers;

/// <summary>
/// 连接控制器
/// </summary>
[ApiController]
[Route("/api/links")]
public class linksController
{
    private readonly ILinksService _linksService;

    public linksController(ILinksService linksService)
    {
        _linksService = linksService;
    }

    /// <summary>
    /// 生成短链接
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [HttpPost("create")]
    public async Task<IRe<LinksResponse.CreateResponse>> Create(
        LinksRequest.CreateRequest data)
    {
        return await _linksService.Create(data);
    }
}