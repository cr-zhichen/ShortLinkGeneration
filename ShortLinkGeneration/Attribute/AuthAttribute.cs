using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ShortLinkGeneration.Entity;
using ShortLinkGeneration.Entity.Enum;
using ShortLinkGeneration.Service;

namespace ShortLinkGeneration.Attribute;

/// <summary>
/// 验证JWT Token
/// </summary>
public class AuthAttribute : ActionFilterAttribute
{
    private readonly string _requiredRole;

    public AuthAttribute(string requiredRole = "")
    {
        _requiredRole = requiredRole;
    }

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var token = context.HttpContext.Request.Headers["Authorization"].ToString().Split(' ').Last();

        // 使用服务定位器来获取 IJwtService
        var jwtService = context.HttpContext.RequestServices.GetService<IJwtService>();

        var isValid = await jwtService.ValidateTokenAsync(token, _requiredRole);

        if (!isValid)
        {
            var errorObject = new Error<object>
            {
                Code = Code.TokenError,
                Message = "Token错误"
            };

            context.Result = new JsonResult(errorObject) { StatusCode = 200 };
            return;
        }

        await next();
    }
}