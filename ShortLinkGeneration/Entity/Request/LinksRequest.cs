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
}