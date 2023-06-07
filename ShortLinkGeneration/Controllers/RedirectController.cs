using Microsoft.AspNetCore.Mvc;
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
}