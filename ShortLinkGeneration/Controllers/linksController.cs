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
    public async Task<IRe<LinksResponse.CreateLinkResponse>> Create(
        LinksRequest.CreateLinkRequest data)
    {
        return await _linksService.Create(data);
    }

    /// <summary>
    /// 检测短链接是否可用
    /// </summary>
    /// <returns></returns>
    [HttpPost("detection")]
    public async Task<IRe<LinksResponse.DetectionResponse>> Detection(
        LinksRequest.DetectionRequest data)
    {
        return await _linksService.Detection(data);
    }

    /// <summary>
    /// 获取用户全部短链接列表 
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [Auth]
    [HttpPost("get-all")]
    public async Task<IRe<LinksResponse.GetAllLinkResponse>> GetAll(
        LinksRequest.GetAllLinkRequest data)
    {
        return await _linksService.GetAll(data);
    }

    /// <summary>
    /// 获取指定的短链接的信息
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [Auth]
    [HttpPost("get")]
    public async Task<IRe<LinksResponse.GetLinkResponse>> Get(
        LinksRequest.GetLinkRequest data)
    {
        return await _linksService.Get(data);
    }

    /// <summary>
    /// 搜索短链接
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [Auth]
    [HttpPost("search")]
    public async Task<IRe<LinksResponse.SearchLinkResponse>> Search(
        LinksRequest.SearchLinkRequest data)
    {
        return await _linksService.Search(data);
    }

    /// <summary>
    /// 更新指定的短链接
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [Auth]
    [HttpPost("update")]
    public async Task<IRe<LinksResponse.UpdateLinkResponse>> Update(
        LinksRequest.UpdateLinkRequest data)
    {
        return await _linksService.Update(data);
    }

    /// <summary>
    /// 删除指定的短链接
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [Auth]
    [HttpPost("delete")]
    public async Task<IRe<LinksResponse.DeleteLinkResponse>> Delete(
        LinksRequest.DeleteLinkRequest data)
    {
        return await _linksService.Delete(data);
    }
}