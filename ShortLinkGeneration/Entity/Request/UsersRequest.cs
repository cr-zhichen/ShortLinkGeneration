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
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public int Code { get; set; }
    }

    /// <summary>
    /// 登录请求实体
    /// </summary>
    public class LoginRequest
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

    /// <summary>
    /// 发送验证码请求实体
    /// </summary>
    public class SendCodeRequest
    {
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Username { get; set; }
    }

    /// <summary>
    /// 返回当前登录用户的信息请求实体
    /// </summary>
    public class InfoRequest
    {
    }

    /// <summary>
    /// 更新密码请求实体
    /// </summary>
    public class UpdatePasswordRequest
    {
        /// <summary>
        /// 旧密码
        /// </summary>
        public string OldPassword { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
        public string NewPassword { get; set; }
    }

    /// <summary>
    /// 重置密码请求实体
    /// </summary>
    public class ResetPasswordRequest
    {
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
        public string NewPassword { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public int Code { get; set; }
    }
}