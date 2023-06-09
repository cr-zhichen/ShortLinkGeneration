namespace ShortLinkGeneration.Entity.Request;

/// <summary>
/// 管理员管理链接请求实体
/// </summary>
public class ManagerLinksRequest
{
    /// <summary>
    /// 获取全部链接请求实体
    /// </summary>
    public class GetAllLinkRequest
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
    /// 获取指定链接信息请求实体
    /// </summary>
    public class GetLinkRequest
    {
        /// <summary>
        /// 链接ID
        /// </summary>
        public int LinkID { get; set; }
    }

    /// <summary>
    /// 更新指定链接信息请求实体
    /// </summary>
    public class UpdateLinkRequest
    {
        /// <summary>
        /// 链接ID
        /// </summary>
        public int LinkID { get; set; }

        /// <summary>
        /// 链接信息
        /// </summary>
        public LinkItemRequest Link { get; set; }
    }

    /// <summary>
    /// 删除链接请求实体
    /// </summary>
    public class DeleteLinkRequest
    {
        /// <summary>
        /// 链接ID
        /// </summary>
        public int LinkID { get; set; }
    }

    /// <summary>
    /// 禁用链接请求实体
    /// </summary>
    public class DisabledLinkRequest
    {
        /// <summary>
        /// 链接ID
        /// </summary>
        public int LinkID { get; set; }
        
        /// <summary>
        /// 是否禁用
        /// </summary>
        public bool IsDisabled { get; set; }
    }

    /// <summary>
    /// 模糊搜索链接请求实体
    /// </summary>
    public class SearchLinkRequest
    {
        /// <summary>
        /// 搜素关键词
        /// </summary>
        public string keywords { get; set; }

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
    /// 获取指定用户链接请求实体
    /// </summary>
    public class GetLinkByUserRequest
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }

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
    /// 获取指定链接点击记录请求实体
    /// </summary>
    public class GetClicksRequest
    {
        /// <summary>
        /// 链接ID
        /// </summary>
        public int LinkID { get; set; }

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
    /// 链接列表项
    /// </summary>
    public class LinkItemRequest
    {
        /// <summary>
        /// 长链接
        /// </summary>
        public string LongLink { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// 最大点击量（null为不限制）
        /// </summary>
        public int? MaxClicks { get; set; }
    }
}