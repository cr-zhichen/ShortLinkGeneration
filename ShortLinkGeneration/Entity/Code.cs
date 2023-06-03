namespace ShortLinkGeneration.Entity;

public enum Code
{
    Error = -1, //未知错误
    Success = 0, //成功
    ConnectionFailedDb = 1,
    InvalidToken = 2, //无效的Token
    InvalidUsernameOrPassword = 3, //无效的用户名或密码
}