namespace ShortLinkGeneration.Entity.Response;

/// <summary>
/// 链接响应实体
/// </summary>
public class LinksResponse
{
    /// <summary>
    /// 创建链接响应实体
    /// </summary>
    public class CreateLinkResponse
    {
        /// <summary>
        /// 短链接
        /// </summary>
        public string ShortLink { get; set; }
    }

    /// <summary>
    /// 检查链接响应实体
    /// </summary>
    public class DetectionResponse
    {
        /// <summary>
        /// 链接是否可用
        /// </summary>
        public bool IsAvailable { get; set; }
    }

    /// <summary>
    /// 获取用户全部短连接列表响应实体
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
    /// 获取指定短链接信息响应实体
    /// </summary>
    public class GetLinkResponse
    {
        /// <summary>
        /// 链接信息
        /// </summary>
        public LinkItemResponse Link { get; set; }
    }

    /// <summary>
    /// 搜索链接响应实体
    /// </summary>
    public class SearchLinkResponse
    {
        /// <summary>
        /// 链接信息
        /// </summary>
        public List<LinkItemResponse> LinkList { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount { get; set; }
    }

    /// <summary>
    /// 更新链接响应实体
    /// </summary>
    public class UpdateLinkResponse
    {
        /// <summary>
        /// 链接信息
        /// </summary>
        public LinkItemResponse Link { get; set; }
    }

    /// <summary>
    /// 删除链接响应实体
    /// </summary>
    public class DeleteLinkResponse
    {
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
}