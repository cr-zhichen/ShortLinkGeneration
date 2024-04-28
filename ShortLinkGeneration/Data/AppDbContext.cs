using Microsoft.EntityFrameworkCore;

namespace ShortLinkGeneration;

/// <summary>
/// 数据库上下文
/// </summary>
public class AppDbContext : DbContext
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="options"></param>
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// UserInfo 表
    /// </summary>
    public DbSet<UserInfoTable> UserinfoTable { get; set; }

    /// <summary>
    /// ShortLinkGeneration 表
    /// </summary>
    public DbSet<ShortLinkGenerationTable> ShortLinkGenerationTable { get; set; }


    /// <summary>
    /// 数据库配置
    /// </summary>
    /// <param name="optionsBuilder"></param>
    override protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    /// <summary>
    /// 配置模型
    /// </summary>
    /// <param name="modelBuilder"></param>
    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        // 配置 UserInfo 表的映射
        modelBuilder
            .Entity<UserInfoTable>(entity =>
            {
                entity.HasKey(e => e.UserId).HasName("PRIMARY");
                entity.Property(e => e.UserId).ValueGeneratedOnAdd();
                entity.HasIndex(u => u.Username).IsUnique();
                entity.Property(e => e.Role).HasConversion<string>();
            });

        // 配置 ShortLinkGeneration 表的映射
        modelBuilder
            .Entity<ShortLinkGenerationTable>(entity =>
            {
                entity.HasKey(e => e.ShortId).HasName("PRIMARY");
                entity.Property(e => e.ShortId).ValueGeneratedOnAdd();
                entity.Property(e => e.CreateTime).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.HasIndex(u => u.ShortUrlSuffix).IsUnique();
            });
    }
}
