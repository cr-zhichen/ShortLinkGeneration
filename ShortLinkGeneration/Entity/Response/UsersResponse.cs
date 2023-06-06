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
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }
    }

    /// <summary>
    /// 登录响应实体
    /// </summary>
    public class LoginResponse
    {
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }
    }

    /// <summary>
    /// 发送验证码响应实体
    /// </summary>
    public class SendCodeResponse
    {
    }

    /// <summary>
    /// 返回当前登录用户的信息响应实体
    /// </summary>
    public class InfoResponse{
        /// <summary>
        /// 用户邮箱
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// 用户权限
        /// </summary>
        public string Role { get; set; }
        /// <summary>
        /// 用户创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 用户创建的短链接数量
        /// </summary>
        public int LinkCount { get; set; }
    }
}