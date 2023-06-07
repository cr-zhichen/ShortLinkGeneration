namespace ShortLinkGeneration.Entity.Enum;

public enum Code
{
    /// <summary>
    /// 未知错误
    /// </summary>
    Error = -1,

    /// <summary>
    /// 成功
    /// </summary>
    Success = 0,

    /// <summary>
    /// 数据库连接失败
    /// </summary>
    ConnectionFailedDb = 1,

    /// <summary>
    /// 管理员账户已存在
    /// </summary>
    AdminAccountAlreadyExists = 2,

    /// <summary>
    /// 用户名或密码格式错误
    /// </summary>
    UsernameOrPasswordFormatError = 3,

    /// <summary>
    /// 密码错误
    /// </summary>
    PasswordError = 4,

    /// <summary>
    /// 账号不存在
    /// </summary>
    UsernameNotExist = 5,

    /// <summary>
    /// 验证码发送错误
    /// </summary>
    SendCodeError = 6,

    /// <summary>
    /// 邮箱格式错误
    /// </summary>
    EmailFormatError = 7,

    /// <summary>
    /// 用户名已存在
    /// </summary>
    UsernameExist = 8,

    /// <summary>
    /// 验证码错误
    /// </summary>
    CodeError = 9,

    /// <summary>
    /// Token错误
    /// </summary>
    TokenError = 10,

    /// <summary>
    /// 密码格式错误
    /// </summary>
    PasswordFormatError = 11,

    /// <summary>
    /// 短连接重复
    /// </summary>
    ShortLinkExists = 12,

    /// <summary>
    /// 短连接生成失败
    /// </summary>
    ShortLinkGenerationFailed = 13,

    /// <summary>
    /// 链接不存在
    /// </summary>
    ShortLinkNotExist = 14,
    
    /// <summary>
    /// 长链接格式错误
    /// </summary>
    LongLinkFormatError = 15,
}