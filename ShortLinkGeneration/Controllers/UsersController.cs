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
    public IRe<UsersResponse.RegisterResponse> Register(
        UsersRequest.RegisterRequest data)
    {
        return _usersService.Register(data);
    }

    /// <summary>
    /// 用户登录
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [HttpPost("login")]
    public IRe<UsersResponse.LoginResponse> Login(
        UsersRequest.LoginRequest data)
    {
        return _usersService.Login(data);
    }

    /// <summary>
    /// 发送验证码
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [HttpPost("sendCode")]
    public IRe<UsersResponse.SendCodeResponse> SendCode(
        UsersRequest.SendCodeRequest data)
    {
        return _usersService.SendCode(data);
    }

    /// <summary>
    /// 返回当前登录用户的信息
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [Auth]
    [HttpPost("info")]
    public IRe<UsersResponse.InfoResponse> Info(
        UsersRequest.InfoRequest data)
    {
        return _usersService.Info(data);
    }

}