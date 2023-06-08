namespace ShortLinkGeneration.Entity.Response;

/// <summary>
/// 管理员管理用户响应实体
/// </summary>
public class ManageUsersResponse
{
    /// <summary>
    /// 获取全部用户响应实体
    /// </summary>
    public class GetAllUserResponse
    {
        /// <summary>
        /// 用户信息列表
        /// </summary>
        public List<UserItemResponse> UsersList { get; set; }
        
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount { get; set; }
    }

    /// <summary>
    /// 创建新用户响应实体
    /// </summary>
    public class CreateUserResponse
    {
    }

    /// <summary>
    /// 获取指定用户信息响应实体
    /// </summary>
    public class GetUserResponse
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        public UserItemResponse User { get; set; }
    }

    /// <summary>
    /// 删除指定用户响应实体
    /// </summary>
    public class DeleteUserResponse
    {
    }

    /// <summary>
    /// 重置密码响应实体
    /// </summary>
    public class RestUserPasswordResponse
    {
    }

    /// <summary>
    /// 搜索用户响应实体
    /// </summary>
    public class SearchUserResponse
    {
        /// <summary>
        /// 用户信息列表
        /// </summary>
        public List<UserItemResponse> UsersList { get; set; }
        
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount { get; set; }
    }

    /// <summary>
    /// 用户信息列表项
    /// </summary>
    public class UserItemResponse
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }

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
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 创造连接数量
        /// </summary>
        public int LinkCount { get; set; }
    }
}