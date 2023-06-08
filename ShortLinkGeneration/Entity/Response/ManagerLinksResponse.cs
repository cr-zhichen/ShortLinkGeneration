namespace ShortLinkGeneration.Entity.Response;

/// <summary>
/// 管理员管理链接响应实体
/// </summary>
public class ManagerLinksResponse
{
    /// <summary>
    /// 获取全部链接响应实体
    /// </summary>
    public class GetAllLinkResponse
    {
        /// <summary>
        /// 连接列表
        /// </summary>
        public List<LinkItemResponse> LinkList { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount { get; set; }
    }

    /// <summary>
    /// 获取指定链接信息响应实体
    /// </summary>
    public class GetLinkResponse
    {
        /// <summary>
        /// 链接信息
        /// </summary>
        public LinkItemResponse Link { get; set; }
    }

    /// <summary>
    /// 更新指定链接信息响应实体
    /// </summary>
    public class UpdateLinkResponse
    {
    }

    /// <summary>
    /// 删除链接响应实体
    /// </summary>
    public class DeleteLinkResponse
    {
    }

    /// <summary>
    /// 禁用链接响应实体
    /// </summary>
    public class DisabledLinkResponse
    {
    }

    /// <summary>
    /// 模糊搜索链接响应实体
    /// </summary>
    public class SearchLinkResponse
    {
        /// <summary>
        /// 连接列表
        /// </summary>
        public List<LinkItemResponse> LinkList { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount { get; set; }
    }

    /// <summary>
    /// 获取用户全部链接响应实体
    /// </summary>
    public class GetLinkByUserResponse
    {
        /// <summary>
        /// 连接列表
        /// </summary>
        public List<LinkItemResponse> LinkList { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount { get; set; }
    }

    /// <summary>
    /// 获取用户指定链接点击记录响应实体
    /// </summary>
    public class GetClicksResponse
    {
        /// <summary>
        /// 点击记录列表
        /// </summary>
        public List<ClicksItemResponse> ClicksList { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount { get; set; }
    }

    /// <summary>
    /// 链接列表项
    /// </summary>
    public class LinkItemResponse
    {
        /// <summary>
        /// 链接ID
        /// </summary>
        public int LinkID { get; set; }

        /// <summary>
        /// 短链接
        /// </summary>
        public string ShortLink { get; set; }

        /// <summary>
        /// 长链接
        /// </summary>
        public string LongLink { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// 点击次数
        /// </summary>
        public int ClickCount { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// 最大点击量（null为不限制）
        /// </summary>
        public int? MaxClicks { get; set; }

        /// <summary>
        /// 是否被禁用
        /// </summary>
        public bool IsDisabled { get; set; }
    }

    /// <summary>
    /// 点击记录列表项
    /// </summary>
    public class ClicksItemResponse
    {
        /// <summary>
        /// 点击记录ID
        /// </summary>
        public int ClickID { get; set; }

        /// <summary>
        /// 所属链接ID
        /// </summary>
        public int LinkID { get; set; }

        /// <summary>
        /// 点击时间
        /// </summary>
        public DateTime ClickTime { get; set; }

        /// <summary>
        /// 点击IP
        /// </summary>
        public string? SourceIP { get; set; }
    }
}