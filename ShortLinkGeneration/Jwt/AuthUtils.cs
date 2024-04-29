using Blazored.LocalStorage;
using ShortLinkGeneration.Infrastructure;
using Microsoft.AspNetCore.Components;

namespace ShortLinkGeneration.Jwt;

[AddScoped]
public class AuthUtils(
    NavigationManager navigationManager,
    ILocalStorageService localStorage,
    IJwtService jwtService,
    IUserSessionService userSessionService
)
{
    /// <summary>
    /// 验证用户身份
    /// </summary>
    /// <param name="roles"></param>
    /// <returns></returns>
    public async Task<bool> Auth(params UserRole[] roles)
    {
        return await Auth(roles as IEnumerable<UserRole>);
    }

    /// <summary>
    /// 验证用户身份
    /// </summary>
    /// <param name="roles"></param>
    /// <returns></returns>
    private async Task<bool> Auth(IEnumerable<UserRole>? roles = null)
    {
        string? token;

        if (String.IsNullOrEmpty(userSessionService.Token))
        {
            token = await localStorage.GetItemAsync<string>("token");
            userSessionService.Token = token;
        }
        else
        {
            token = userSessionService.Token;
        }

        if (string.IsNullOrEmpty(token))
        {
            userSessionService.PreviousRouteBeforeLogin = new Uri(navigationManager.Uri).AbsolutePath;
            navigationManager.NavigateTo("/login");
            return false;
        }

        bool isValid = false;

        // 检查令牌是否有效于提供的任一角色
        if (roles != null)
        {
            foreach (var role in roles)
            {
                if (await jwtService.ValidateTokenAsync(token, role.ToString()))
                {
                    isValid = true;
                    break;
                }
            }
        }
        else
        {
            isValid = await jwtService.ValidateTokenAsync(token);
        }

        if (!isValid)
        {
            userSessionService.PreviousRouteBeforeLogin = new Uri(navigationManager.Uri).AbsolutePath;
            navigationManager.NavigateTo("/login");
            userSessionService.Token = null;
            await localStorage.RemoveItemAsync("token");
            return false;
        }

        return true;
    }
}
