namespace ShortLinkGeneration.Infrastructure;

/// <summary>
/// 用户会话服务接口
/// </summary>
public interface IUserSessionService
{
    /// <summary>
    /// 用户令牌
    /// </summary>
    string? Token { get; set; }

    /// <summary>
    /// 用户登录跳转前路由
    /// </summary>
    string PreviousRouteBeforeLogin { get; set; }
}

/// <summary>
/// 用户会话服务
/// </summary>
[AddScopedAsImplementedInterfaces]
public class UserSessionService : IUserSessionService
{
    public string? Token { get; set; } = null;
    public string PreviousRouteBeforeLogin { get; set; } = "/";
}
