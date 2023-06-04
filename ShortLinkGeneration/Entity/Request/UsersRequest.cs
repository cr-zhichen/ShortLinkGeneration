namespace ShortLinkGeneration.Entity.Request;

/// <summary>
/// 用户请求实体
/// </summary>
public class UsersRequest
{
    /// <summary>
    /// 注册请求实体
    /// </summary>
    public class RegisterRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}