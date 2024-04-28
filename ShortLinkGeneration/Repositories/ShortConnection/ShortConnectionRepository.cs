using Microsoft.EntityFrameworkCore;
using ShortLinkGeneration.Infrastructure;

namespace ShortLinkGeneration.Repositories.ShortLinkGeneration;

/// <summary>
/// 和ShortLinkGeneration相关的数据库操作
/// </summary>
[AddScopedAsImplementedInterfaces]
public class ShortLinkGenerationRepository : IShortLinkGenerationRepository
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    private readonly AppDbContext _context;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="context"></param>
    public ShortLinkGenerationRepository(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// 增加短链接
    /// </summary>
    /// <param name="longUrl"></param>
    /// <param name="shortUrl"></param>
    /// <returns></returns>
    public async Task<ShortLinkGenerationTable> AddShortLinkGenerationAsync(string longUrl, string shortUrl)
    {
        // 判断短链接是否已存在
        ShortLinkGenerationTable? ShortLinkGeneration = await _context.ShortLinkGenerationTable.FirstOrDefaultAsync(x => x.ShortUrlSuffix == shortUrl);

        if (ShortLinkGeneration != null)
        {
            throw new Exception("短链接已存在");
        }

        _context.ShortLinkGenerationTable.Add(new ShortLinkGenerationTable
        {
            LongUrl = longUrl,
            ShortUrlSuffix = shortUrl,
            CreateTime = DateTime.Now,
            ClickCount = 0
        });
        await _context.SaveChangesAsync();

        return await GetShortLinkGenerationAsync(shortUrl);
    }

    /// <summary>
    /// 获取短链接
    /// </summary>
    /// <param name="shortUrl"></param>
    /// <returns></returns>
    public async Task<ShortLinkGenerationTable> GetShortLinkGenerationAsync(string shortUrl)
    {
        ShortLinkGenerationTable? ShortLinkGeneration = await _context.ShortLinkGenerationTable.FirstOrDefaultAsync(x => x.ShortUrlSuffix == shortUrl);

        if (ShortLinkGeneration == null)
        {
            throw new Exception("短链接不存在");
        }

        return ShortLinkGeneration;

    }

    /// <summary>
    /// 删除短链接
    /// </summary>
    /// <param name="shortId"></param>
    /// <returns></returns>
    public async Task DeleteShortLinkGenerationAsync(int shortId)
    {
        ShortLinkGenerationTable? ShortLinkGeneration = await _context.ShortLinkGenerationTable.FirstOrDefaultAsync(x => x.ShortId == shortId);

        if (ShortLinkGeneration == null)
        {
            throw new Exception("短链接不存在");
        }

        _context.ShortLinkGenerationTable.Remove(ShortLinkGeneration);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// 获取短链接列表
    /// </summary>
    /// <returns></returns>
    public async Task<List<ShortLinkGenerationTable>> GetShortLinkGenerationListAsync()
    {
        return await _context.ShortLinkGenerationTable.ToListAsync();
    }

    /// <summary>
    /// 增加短链接点击次数
    /// </summary>
    /// <param name="shortId"></param>
    /// <returns></returns>
    public async Task AddClickCountAsync(int shortId)
    {
        ShortLinkGenerationTable? ShortLinkGeneration = await _context.ShortLinkGenerationTable.FirstOrDefaultAsync(x => x.ShortId == shortId);

        if (ShortLinkGeneration == null)
        {
            throw new Exception("短链接不存在");
        }

        ShortLinkGeneration.ClickCount++;
        await _context.SaveChangesAsync();
    }
}
