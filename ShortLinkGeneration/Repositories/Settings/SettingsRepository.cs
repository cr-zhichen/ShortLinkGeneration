using Microsoft.EntityFrameworkCore;
using ShortLinkGeneration.Infrastructure;

namespace ShortLinkGeneration.Repositories.Settings;

[AddScopedAsImplementedInterfaces]
public class SettingsRepository : ISettingsRepository
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    private readonly AppDbContext _context;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="context"></param>
    public SettingsRepository(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// 添加或更新一个配置
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public async Task AddOrUpdateSettingAsync(string name, string value)
    {
        var setting = await _context.SettingsTables.FindAsync(name);
        if (setting == null)
        {
            setting = new SettingsTables()
            {
                Name = name,
                Value = value
            };
            await _context.SettingsTables.AddAsync(setting);
        }
        else
        {
            setting.Value = value;
        }

        await _context.SaveChangesAsync();
    }
    /// <summary>
    /// 获取一个配置
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public async Task<string?> GetSettingAsync(string name)
    {
        var setting = await _context.SettingsTables.FindAsync(name);
        return setting?.Value;
    }

    /// <summary>
    /// 删除一个配置
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public async Task DeleteSettingAsync(string name)
    {
        var setting = await _context.SettingsTables.FindAsync(name);
        if (setting == null)
        {
            throw new KeyNotFoundException();
        }

        _context.SettingsTables.Remove(setting);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// 获取所有配置
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<(string name, string value)>> GetAllSettingsAsync()
    {
        // 获取全部配置
        var settings = await _context.SettingsTables.ToListAsync();
        return settings.Select(setting => (setting.Name, setting.Value));
    }
}
