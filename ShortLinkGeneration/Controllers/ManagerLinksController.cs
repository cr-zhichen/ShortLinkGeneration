using Microsoft.AspNetCore.Mvc;
using ShortLinkGeneration.Attribute;
using ShortLinkGeneration.Entity;
using ShortLinkGeneration.Entity.Enum;
using ShortLinkGeneration.Entity.Request;
using ShortLinkGeneration.Entity.Response;
using ShortLinkGeneration.Service.Service;

namespace ShortLinkGeneration.Controllers;

/// <summary>
/// 管理员管理链接
/// </summary>
[ApiController]
[Route("/api/admin/links")]
public class ManagerLinksController
{
    private readonly IManagerLinksServer _managerLinksServer;

    public ManagerLinksController(IManagerLinksServer managerLinksServer)
    {
        _managerLinksServer = managerLinksServer;
    }

    /// <summary>
    /// 获取全部链接
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [Auth(Role.Admin)]
    [HttpPost("get-all")]
    public async Task<IRe<ManagerLinksResponse.GetAllLinkResponse>> GetAll(
        ManagerLinksRequest.GetAllLinkRequest data)
    {
        return await _managerLinksServer.GetAll(data);
    }

    /// <summary>
    /// 获取指定链接信息
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [Auth(Role.Admin)]
    [HttpPost("get")]
    public async Task<IRe<ManagerLinksResponse.GetLinkResponse>> Get(
        ManagerLinksRequest.GetLinkRequest data)
    {
        return await _managerLinksServer.Get(data);
    }

    /// <summary>
    /// 更新指定链接信息
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [Auth(Role.Admin)]
    [HttpPost("update")]
    public async Task<IRe<ManagerLinksResponse.UpdateLinkResponse>> Update(
        ManagerLinksRequest.UpdateLinkRequest data)
    {
        return await _managerLinksServer.Update(data);
    }

    /// <summary>
    /// 删除指定链接
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [Auth(Role.Admin)]
    [HttpPost("delete")]
    public async Task<IRe<ManagerLinksResponse.DeleteLinkResponse>> Delete(
        ManagerLinksRequest.DeleteLinkRequest data)
    {
        return await _managerLinksServer.Delete(data);
    }

    /// <summary>
    /// 禁用指定链接
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [Auth(Role.Admin)]
    [HttpPost("disabled")]
    public async Task<IRe<ManagerLinksResponse.DisabledLinkResponse>> Disabled(
        ManagerLinksRequest.DisabledLinkRequest data)
    {
        return await _managerLinksServer.Disabled(data);
    }

    /// <summary>
    /// 模糊搜索链接
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [Auth(Role.Admin)]
    [HttpPost("search")]
    public async Task<IRe<ManagerLinksResponse.SearchLinkResponse>> Search(
        ManagerLinksRequest.SearchLinkRequest data)
    {
        return await _managerLinksServer.Search(data);
    }

    /// <summary>
    /// 获取指定用户的所有链接
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [Auth(Role.Admin)]
    [HttpPost("get-by-user")]
    public async Task<IRe<ManagerLinksResponse.GetLinkByUserResponse>> GetByUser(
        ManagerLinksRequest.GetLinkByUserRequest data)
    {
        return await _managerLinksServer.GetByUser(data);
    }

    /// <summary>
    /// 获取指定链接的点击记录
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [Auth(Role.Admin)]
    [HttpPost("get-clicks")]
    public async Task<IRe<ManagerLinksResponse.GetClicksResponse>> GetClicks(
        ManagerLinksRequest.GetClicksRequest data)
    {
        return await _managerLinksServer.GetClicks(data);
    }
}