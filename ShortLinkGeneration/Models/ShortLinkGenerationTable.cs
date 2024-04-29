using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShortLinkGeneration;

/// <summary>
/// 表示数据库中的short_connection表。
/// </summary>
[Table("short_link_generation")]
public class ShortLinkGenerationTable
{
    /// <summary>
    /// 主键 ID
    /// </summary>
    [Key]
    [Column("short_id")]
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    public int ShortId { get; set; }

    /// <summary>
    /// 长链接
    /// </summary>
    [Required]
    [StringLength(1000)]
    [Column("long_url")]
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    public string LongUrl { get; set; } = null!;

    /// <summary>
    /// 短链接
    /// </summary>
    [Required]
    [StringLength(1000)]
    [Column("short_url_suffix")]
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    public string ShortUrlSuffix { get; set; } = null!;

    /// <summary>
    /// 创建时间
    /// </summary>
    [Required]
    [Column("create_time")]
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 点击次数
    /// </summary>
    [Required]
    [Column("click_count")]
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    public int ClickCount { get; set; }
}
