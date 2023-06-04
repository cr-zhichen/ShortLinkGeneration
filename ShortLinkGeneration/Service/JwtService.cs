using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ShortLinkGeneration.Service;

// 接口
public interface IJwtService
{
    /// <summary>
    /// 创建令牌
    /// </summary>
    /// <param name="username"></param>
    /// <param name="role"></param>
    /// <returns></returns>
    Task<string> CreateTokenAsync(string username, string role);

    /// <summary>
    /// 验证令牌
    /// </summary>
    /// <param name="token"></param>
    /// <param name="requiredRole"></param>
    /// <returns></returns>
    Task<bool> ValidateTokenAsync(string token, string requiredRole = "");
}

// 实现
public class JwtService : IJwtService
{
    public Tool.JWT.TokenOptions TokenOptions { get; }

    public JwtService(IOptions<Tool.JWT.TokenOptions> options)
    {
        TokenOptions = options.Value;
    }

    public Task<string> CreateTokenAsync(string username, string role)
    {
        // 添加一些需要的键值对
        Claim[] claims = new[]
        {
            new Claim("user", username),
            new Claim("role", role)
        };

        var keyBytes = Encoding.UTF8.GetBytes(TokenOptions.SecretKey);
        var creds = new SigningCredentials(new SymmetricSecurityKey(keyBytes),
            SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: TokenOptions.Issuer, // 签发者
            audience: TokenOptions.Audience, // 接收者
            claims: claims, // payload
            expires: DateTime.Now.AddMinutes(TokenOptions.ExpireMinutes), // 过期时间
            signingCredentials: creds); // 令牌

        var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        return Task.FromResult(token);
    }

    public Task<bool> ValidateTokenAsync(string token, string requiredRole)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenOptions.SecretKey)),
            ValidateIssuer = true,
            ValidIssuer = TokenOptions.Issuer,
            ValidateAudience = true,
            ValidAudience = TokenOptions.Audience
        };

        try
        {
            tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

            // Now we get the token and check the role claim
            var jwtToken = (JwtSecurityToken)validatedToken;
            var roleClaim = jwtToken.Claims.First(claim => claim.Type == "role");

            // Check if the role claim is the expected one
            if (roleClaim.Value != requiredRole)
            {
                return Task.FromResult(false);
            }
        }
        catch
        {
            return Task.FromResult(false);
        }

        return Task.FromResult(true);
    }
}