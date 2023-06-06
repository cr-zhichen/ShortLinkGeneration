// 引入 Entity Framework Core 命名空间

using Microsoft.EntityFrameworkCore;

namespace ShortLinkGeneration.DB
{
    // 定义一个名为 ShortLinkContext 的类，继承自 DbContext
    public class ShortLinkContext : DbContext
    {
        // 构造函数，接收一个 DbContextOptions<ShortLinkContext> 类型的参数，使用依赖注入的方式来配置 DbContext
        public ShortLinkContext(DbContextOptions<ShortLinkContext> options)
            : base(options)
        {
        }

        // 定义 DbSet 类型的属性，表示在数据库中可以查询的 User 类型的集合
        public DbSet<User> Users { get; set; }

        // 定义 DbSet 类型的属性，表示在数据库中可以查询的 Link 类型的集合
        public DbSet<Link> Links { get; set; }

        // 定义 DbSet 类型的属性，表示在数据库中可以查询的 Click 类型的集合
        public DbSet<Click> Clicks { get; set; }

        // 重写父类 DbContext 的 OnModelCreating 方法，用于配置实体与数据库的映射
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 配置 User 实体的 Role 属性在数据库中存储为字符串
            // HasConversion 方法用于设置类型转换，这里将 Role 属性的类型在数据库中设置为字符串
            modelBuilder.Entity<User>()
                .Property(e => e.Role)
                .HasConversion<string>();
            
            modelBuilder.Entity<Link>()
                .HasOne(p => p.User)
                .WithMany(b => b.Links)
                .HasForeignKey(p => p.UserID)
                .IsRequired(false); // 添加这一行来允许外键为空
        }
    }
}