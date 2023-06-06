namespace ShortLinkGeneration.Entity.Response;

/// <summary>
/// 链接响应实体
/// </summary>
public class LinksResponse
{
    /// <summary>
    /// 创建链接响应实体
    /// </summary>
    public class CreateResponse
    {
        /// <summary>
        /// 短链接
        /// </summary>
        public string ShortLink { get; set; }
    }
}