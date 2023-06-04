using Microsoft.AspNetCore.Mvc;
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
}