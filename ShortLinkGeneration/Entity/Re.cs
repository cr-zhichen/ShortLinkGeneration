namespace ShortLinkGeneration.Entity;

public interface IRe<T>
{
    /// <summary>
    /// 状态码
    /// </summary>
    public Code Code { get; set; }

    /// <summary>
    /// 消息
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// 返回数据
    /// </summary>
    public T? Data { get; set; }
}

public class Ok<T> : IRe<T>
{
    /// <summary>
    /// 状态码，默认为成功
    /// </summary>
    public Code Code { get; set; } = Code.Success;

    public string? Message { get; set; }
    public T? Data { get; set; }
}

public class Error<T> : IRe<T>
{
    /// <summary>
    /// 状态码，默认为未知错误
    /// </summary>
    public Code Code { get; set; } = Code.Error;

    public string? Message { get; set; }
    public T? Data { get; set; }
}