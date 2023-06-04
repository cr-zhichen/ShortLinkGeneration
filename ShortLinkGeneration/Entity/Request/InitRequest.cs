namespace ShortLinkGeneration.Entity.Request;

/// <summary>
/// 初始化请求实体
/// </summary>
public class InitRequest
{
    /// <summary>
    /// 初始化数据库请求实体
    /// </summary>
    public class InitDbRequest
    {
    }

    /// <summary>
    /// 初始化管理员请求实体
    /// </summary>
    public class InitAdminRequest
    {
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }
}