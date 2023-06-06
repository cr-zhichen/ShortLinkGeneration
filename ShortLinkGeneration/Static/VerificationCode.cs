namespace ShortLinkGeneration.Static;

/// <summary>
/// 验证码保存类
/// </summary>
public static class VerificationCode
{
    public static List<VerificationCodeItem> VerificationCodeList = new();

    /// <summary>
    /// 验证码暂存
    /// </summary>
    public class VerificationCodeItem
    {
        public string Email { get; set; }
        public DateTime ExpireTime { get; set; }
        public int Code { get; set; }
    }
}