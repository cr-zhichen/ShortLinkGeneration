using Microsoft.AspNetCore.Mvc;
using ShortLinkGeneration.Attribute;
using ShortLinkGeneration.Entity;
using ShortLinkGeneration.Entity.Enum;
using ShortLinkGeneration.Entity.Request;
using ShortLinkGeneration.Entity.Response;
using ShortLinkGeneration.Service.Service;

namespace ShortLinkGeneration.Controllers;

/// <summary>
/// 管理员管理用户
/// </summary>
[ApiController]
[Route("/api/admin/user")]
public class ManageUsersController
{
    private readonly IManageUsersService _manageUsersService;

    public ManageUsersController(IManageUsersService manageUsersService)
    {
        _manageUsersService = manageUsersService;
    }

    /// <summary>
    /// 获取全部用户
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [Auth(Role.Admin)]
    [HttpPost("get-all")]
    public async Task<IRe<ManageUsersResponse.GetAllUserResponse>> GetAll(
        ManageUsersRequest.GetAllUserRequest data)
    {
        return await _manageUsersService.GetAll(data);
    }

    /// <summary>
    /// 创建一个新用户
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [Auth(Role.Admin)]
    [HttpPost("create")]
    public async Task<IRe<ManageUsersResponse.CreateUserResponse>> Create(
        ManageUsersRequest.CreateUserRequest data)
    {
        return await _manageUsersService.Create(data);
    }

    /// <summary>
    /// 获取指定用户信息
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [Auth(Role.Admin)]
    [HttpPost("get")]
    public async Task<IRe<ManageUsersResponse.GetUserResponse>> Get(
        ManageUsersRequest.GetUserRequest data)
    {
        return await _manageUsersService.Get(data);
    }

    /// <summary>
    /// 删除指定用户
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [Auth(Role.Admin)]
    [HttpPost("delete")]
    public async Task<IRe<ManageUsersResponse.DeleteUserResponse>> Delete(
        ManageUsersRequest.DeleteUserRequest data)
    {
        return await _manageUsersService.Delete(data);
    }

    /// <summary>
    /// 重置指定用户密码
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [Auth(Role.Admin)]
    [HttpPost("reset-password")]
    public async Task<IRe<ManageUsersResponse.RestUserPasswordResponse>> RestPassword(
        ManageUsersRequest.RestUserPasswordRequest data)
    {
        return await _manageUsersService.RestPassword(data);
    }

    /// <summary>
    /// 搜索用户
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [Auth(Role.Admin)]
    [HttpPost("search")]
    public async Task<IRe<ManageUsersResponse.SearchUserResponse>> Search(
        ManageUsersRequest.SearchUserRequest data)
    {
        return await _manageUsersService.Search(data);
    }
}