using System.Text;
using Konscious.Security.Cryptography;

namespace ShortLinkGeneration.Tool;

/// <summary>
/// Argon2加密
/// </summary>
public static class Argon2Hasher
{
    /// <summary>
    /// 给密码加密
    /// </summary>
    /// <param name="password"></param>
    /// <param name="salt"></param>
    /// <returns></returns>
    public static string HashPassword(this string password, string salt)
    {
        byte[] saltBytes = Encoding.UTF8.GetBytes(salt);

        using (var hasher = new Argon2id(Encoding.UTF8.GetBytes(password)))
        {
            hasher.Salt = saltBytes;
            hasher.DegreeOfParallelism = 8; // 根据您的硬件设置
            hasher.MemorySize = 65536; // 64 MB
            hasher.Iterations = 4;

            byte[] hashBytes = hasher.GetBytes(32); // 获取32字节的哈希值

            return Convert.ToBase64String(hashBytes);
        }
    }

    /// <summary>
    /// 验证密码
    /// </summary>
    /// <param name="password"></param>
    /// <param name="storedHash"></param>
    /// <param name="salt"></param>
    /// <returns></returns>
    public static bool VerifyPassword(this string password, string storedHash, string salt)
    {
        string hashToVerify = HashPassword(password, salt);
        return storedHash.Equals(hashToVerify);
    }
}