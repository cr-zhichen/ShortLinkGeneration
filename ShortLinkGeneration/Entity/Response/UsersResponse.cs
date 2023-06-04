namespace ShortLinkGeneration.Entity.Response;

/// <summary>
/// 用户响应实体
/// </summary>
public class UsersResponse
{
    /// <summary>
    /// 注册响应实体
    /// </summary>
    public class RegisterResponse
    {
        public string Username { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}