namespace ShortLinkGeneration.Repositories.ShortLinkGeneration;

/// <summary>
/// 和ShortLinkGeneration相关的数据库操作
/// </summary>
public interface IShortLinkGenerationRepository
{
    /// <summary>
    /// 增加短链接
    /// </summary>
    /// <param name="longUrl"></param>
    /// <param name="shortUrl"></param>
    /// <returns></returns>
    Task<ShortLinkGenerationTable> AddShortLinkGenerationAsync(string longUrl, string shortUrl);

    /// <summary>
    /// 修改短连接
    /// </summary>
    /// <param name="shortId"></param>
    /// <param name="longUrl"></param>
    /// <returns></returns>
    Task<ShortLinkGenerationTable> UpdateShortLinkGenerationAsync(int shortId, string longUrl);

    /// <summary>
    /// 获取短链接
    /// </summary>
    /// <param name="shortUrl"></param>
    /// <returns></returns>
    Task<ShortLinkGenerationTable> GetShortLinkGenerationAsync(string shortUrl);

    /// <summary>
    /// 删除短链接
    /// </summary>
    /// <param name="shortId"></param>
    /// <returns></returns>
    Task DeleteShortLinkGenerationAsync(int shortId);

    /// <summary>
    /// 获取短链接列表
    /// </summary>
    /// <returns></returns>
    Task<List<ShortLinkGenerationTable>> GetShortLinkGenerationListAsync();

    /// <summary>
    /// 增加短链接点击次数
    /// </summary>
    /// <param name="shortId"></param>
    /// <returns></returns>
    Task AddClickCountAsync(int shortId);
}
