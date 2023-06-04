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
}