using Microsoft.AspNetCore.Mvc;
using ShortLinkGeneration.Attribute;
using ShortLinkGeneration.Entity;
using ShortLinkGeneration.Entity.Request;
using ShortLinkGeneration.Entity.Response;
using ShortLinkGeneration.Service.Service;

namespace ShortLinkGeneration.Controllers;

/// <summary>
/// 初始化控制器
/// </summary>
[ApiController]
[Route("/api/users")]
public class UsersController
{
    private readonly IUsersService _usersService;

    public UsersController(IUsersService usersService)
    {
        _usersService = usersService;
    }

    /// <summary>
    /// 用户注册
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [HttpPost("register")]
    public async Task<IRe<UsersResponse.RegisterResponse>> Register(
        UsersRequest.RegisterRequest data)
    {
        return await _usersService.Register(data);
    }

    /// <summary>
    /// 用户登录
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [HttpPost("login")]
    public async Task<IRe<UsersResponse.LoginResponse>> Login(
        UsersRequest.LoginRequest data)
    {
        return await _usersService.Login(data);
    }

    /// <summary>
    /// 发送验证码
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [HttpPost("sendCode")]
    public async Task<IRe<UsersResponse.SendCodeResponse>> SendCode(
        UsersRequest.SendCodeRequest data)
    {
        return await _usersService.SendCode(data);
    }

    /// <summary>
    /// 返回当前登录用户的信息
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [Auth]
    [HttpPost("info")]
    public async Task<IRe<UsersResponse.InfoResponse>> Info(
        UsersRequest.InfoRequest data)
    {
        return await _usersService.Info(data);
    }

    /// <summary>
    /// 更新密码
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [Auth]
    [HttpPost("update-password")]
    public async Task<IRe<UsersResponse.UpdatePasswordResponse>> UpdatePassword(
        UsersRequest.UpdatePasswordRequest data)
    {
        return await _usersService.UpdatePassword(data);
    }

    /// <summary>
    /// 重置密码
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [HttpPost("reset-password")]
    public async Task<IRe<UsersResponse.ResetPasswordResponse>> ResetPassword(
        UsersRequest.ResetPasswordRequest data)
    {
        return await _usersService.ResetPassword(data);
    }
}