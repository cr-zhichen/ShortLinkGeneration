namespace ShortLinkGeneration.Entity.Request;

/// <summary>
/// 链接请求实体
/// </summary>
public class LinksRequest
{
    /// <summary>
    /// 创建链接请求实体
    /// </summary>
    public class CreateRequest
    {
        /// <summary>
        /// 长链接
        /// </summary>
        public string LongLink { get; set; }

        /// <summary>
        /// 目标短链接
        /// </summary>
        public string? ShortLink { get; set; }

        /// <summary>
        /// 连接过期时间，为空则不过期
        /// </summary>
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// 最大点击次数，为空则不限制
        /// </summary>
        public int? MaxClicks { get; set; }
    }

    /// <summary>
    /// 检查链接请求实体
    /// </summary>
    public class DetectionRequest
    {
        /// <summary>
        /// 目标短链接
        /// </summary>
        public string ShortLink { get; set; }
    }

    /// <summary>
    /// 获取用户全部链接请求实体
    /// </summary>
    public class GetAllRequest
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
    /// 获取指定链接请求实体
    /// </summary>
    public class GetRequest
    {
        /// <summary>
        /// 链接ID
        /// </summary>
        public int LinkID { get; set; }
    }

    /// <summary>
    /// 搜索链接请求实体
    /// </summary>
    public class SearchRequest
    {
        /// <summary>
        /// 关键词
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
    /// 删除链接请求实体
    /// </summary>
    public class DeleteRequest
    {
        /// <summary>
        /// 链接ID
        /// </summary>
        public int LinkID { get; set; }
    }

    /// <summary>
    /// 更新链接请求实体
    /// </summary>
    public class UpdateRequest
    {
        /// <summary>
        /// 连接信息
        /// </summary>
        public LinkItemRequest Link { get; set; }
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