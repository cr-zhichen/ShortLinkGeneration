namespace ShortLinkGeneration.Entity.Request;

/// <summary>
/// 连接请求实体
/// </summary>
public class RedirectRequest
{
    public class RedirectPostRequest
    {
        /// <summary>
        /// 短连接
        /// </summary>
        public string ShortLink { get; set; }
    }
}