namespace ShortLinkGeneration.Repositories.Settings;

public interface ISettingsRepository
{
    /// <summary>
    /// 添加或更新一个配置
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    Task AddOrUpdateSettingAsync(string name, string value);

    /// <summary>
    /// 获取一个配置
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    Task<string?> GetSettingAsync(string name);

    /// <summary>
    /// 删除一个配置
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    Task DeleteSettingAsync(string name);

    /// <summary>
    /// 获取所有配置
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<(string name, string value)>> GetAllSettingsAsync();
}
