namespace ShortLinkGeneration.Entity.Request;

/// <summary>
/// 初始化请求实体
/// </summary>
public class InitRequest
{
    public class InitDbRequest
    {
    }

    public class InitAdminRequest
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}