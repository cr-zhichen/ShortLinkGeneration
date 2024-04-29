using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShortLinkGeneration;

/// <summary>
/// 表示数据库中的settings表。
/// </summary>
[Table("settings")]
public class SettingsTables
{
    /// <summary>
    /// 设置名称
    /// </summary>
    [Key]
    [StringLength(100)]
    [Column("name")]
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    public string Name { get; set; } = null!;

    /// <summary>
    /// 设置值
    /// </summary>
    [Required]
    [StringLength(1000)]
    [Column("value")]
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    public string Value { get; set; } = null!;
}
