using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShortLinkGeneration.DB;

public class User
{
    [Key] public int UserID { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    [Column(TypeName = "varchar(10)")]
    public string Role { get; set; }
    public DateTime CreationTime { get; set; }

    // 导航属性
    [InverseProperty("User")]
    public List<Link> Links { get; set; }
}

public class Link
{
    [Key] public int LinkID { get; set; }
    public string ShortLink { get; set; }
    public string OriginalLink { get; set; }
    [ForeignKey("User")]
    public int? UserID { get; set; }
    public DateTime CreationDate { get; set; }
    public int ClickCount { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public int? MaxClicks { get; set; }
    public bool IsDisabled { get; set; }

    // 导航属性
    public User User { get; set; }
    [InverseProperty("Link")]
    public List<Click> Clicks { get; set; }
}

public class Click
{
    [Key] public int ClickID { get; set; }
    [ForeignKey("Link")]
    public int LinkID { get; set; }
    public DateTime ClickTime { get; set; }
    public string? SourceIP { get; set; }

    // 导航属性
    public Link Link { get; set; }
}