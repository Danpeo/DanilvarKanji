using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DanilvarKanji.Infrastructure.Common;
using DanilvarKanji.Infrastructure.Options;
using DanilvarKanji.Shared.Domain.Entities;
using DanilvarKanji.Shared.Enums;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DanilvarKanji.Infrastructure.Auth;

public class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _jwtOptions;
    private readonly IDateTime _dateTime;

    public JwtProvider(IOptions<JwtOptions> jwtOptions, IDateTime dateTime)
    {
        _dateTime = dateTime;
        _jwtOptions = jwtOptions.Value;
    }

    public JwtSecurityToken GenerateJwt(AppUser user)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));

        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var claims = new Claim[]
        {
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Email, user.Email!),
            new(JwtRegisteredClaimNames.Name, user.UserName!),
            new(ClaimTypes.Role, user.Role),
            new(JwtClaim.XP.ToString(), user.XP.ToString())
        };

        DateTime tokenExpirationTime = _dateTime.UtcNow.AddMinutes(_jwtOptions.TokenExpirationInMinutes);

        var token = new JwtSecurityToken(
            _jwtOptions.Issuer,
            _jwtOptions.Audience,
            claims,
            null,
            tokenExpirationTime,
            signingCredentials
        );

        /*string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenValue;*/

        return token;
    }

    public string GetTokenValue(JwtSecurityToken token)
    {
        string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenValue;
    }

    public ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
    {
        string secret = _jwtOptions.SecretKey ?? throw new InvalidOperationException("Secret not configured");

        var validation = new TokenValidationParameters
        {
            ValidIssuer = _jwtOptions.Issuer,
            ValidAudience = _jwtOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
            ValidateLifetime = false
        };

        return new JwtSecurityTokenHandler().ValidateToken(token, validation, out _);
    }

    public string GenerateRefreshToken()
    {
        byte[] randomNumber = new byte[64];

        using var generator = RandomNumberGenerator.Create();

        generator.GetBytes(randomNumber);

        return Convert.ToBase64String(randomNumber);
    }
}