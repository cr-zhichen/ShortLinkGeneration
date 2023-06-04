using System.ComponentModel.DataAnnotations;

namespace ShortLinkGeneration.DB;

public class User
{
    [Key] public int UserID { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public string Role { get; set; }
    public string PasswordResetToken { get; set; }
    public DateTime? PasswordResetTokenExpiry { get; set; }
    public DateTime CreationTime { get; set; }

    // 导航属性
    public List<Link> Links { get; set; }
}

public class Link
{
    [Key] public int LinkID { get; set; }
    public string ShortLink { get; set; }
    public string OriginalLink { get; set; }
    public int UserID { get; set; }
    public DateTime CreationDate { get; set; }
    public int ClickCount { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public int? MaxClicks { get; set; }
    public bool IsDelayed { get; set; }

    // 导航属性
    public User User { get; set; }
    public List<Click> Clicks { get; set; }
}

public class Click
{
    [Key] public int ClickID { get; set; }
    public int LinkID { get; set; }
    public DateTime ClickTime { get; set; }
    public string SourceIP { get; set; }

    // 导航属性
    public Link Link { get; set; }
}