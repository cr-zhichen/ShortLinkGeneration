namespace ShortLinkGeneration.Tool;

public class ConfigOptions
{
    /// <summary>
    /// SMTP服务器
    /// </summary>
    public string SmtpService { get; set; }

    /// <summary>
    /// SMTP端口
    /// </summary>
    public int Port { get; set; }

    /// <summary>
    /// 发件人地址
    /// </summary>
    public string SendEmail { get; set; }

    /// <summary>
    /// 发件人密码
    /// </summary>
    public string SendPwd { get; set; }
}