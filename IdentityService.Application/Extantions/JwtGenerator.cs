using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DotNetEnv;
using IdentityService.Domain.Interfaces.Extantions;
using Microsoft.IdentityModel.Tokens;

namespace IdentityService.Application.Extantions;

public class JwtGenerator : IJwtGenerator
{
    public string GetAccessTokenAsync(string userId)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userId),
        };

        var jwt = new JwtSecurityToken(
            issuer: Env.GetString("JWT_ISSUER"),
            audience: Env.GetString("JWT_AUDIENCE"),
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(Env.GetInt("JWT_LIFETIME_MIN")),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Env.GetString("JWT_SECRET_KEY"))),
                SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    public string GetRefreshTokenAsync()
    {
        return Convert.ToBase64String(GenerateSalt());
    }
    
    private byte[] GenerateSalt()
    {
        var salt = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(salt);

        return salt;
    }
}