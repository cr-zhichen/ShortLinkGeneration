namespace ShortLinkGeneration.Entity.Request;

/// <summary>
/// 管理员管理用户请求实体
/// </summary>
public class ManageUsersRequest
{
    /// <summary>
    /// 获取全部用户请求实体
    /// </summary>
    public class GetAllUserRequest
    {
        /// <summary>
        /// 页数
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 每页数量
        /// </summary>
        public int PageSize { get; set; }
    }

    /// <summary>
    /// 创建新用户请求实体
    /// </summary>
    public class CreateUserRequest
    {
        /// <summary>
        /// 用户邮箱
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string Password { get; set; }
    }

    /// <summary>
    /// 获取指定用户信息请求实体
    /// </summary>
    public class GetUserRequest
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserID { get; set; }
    }

    /// <summary>
    /// 删除指定用户请求实体
    /// </summary>
    public class DeleteUserRequest
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserID { get; set; }
    }

    /// <summary>
    /// 重置密码请求实体
    /// </summary>
    public class RestUserPasswordRequest
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// 用户新密码
        /// </summary>
        public string NewPassword { get; set; }
    }

    /// <summary>
    /// 搜索用户请求实体
    /// </summary>
    public class SearchUserRequest
    {
        /// <summary>
        /// 搜索关键字
        /// </summary>
        public string Keyword { get; set; }

        /// <summary>
        /// 页数
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 每页数量
        /// </summary>
        public int PageSize { get; set; }
    }
}