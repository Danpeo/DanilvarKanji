using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DanilvarKanji.Domain.Entities;
using DanilvarKanji.Infrastructure.Common;
using DanilvarKanji.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DanilvarKanji.Infrastructure.Auth;

public class JwtProvier : IJwtProvider
{
    private readonly JwtOptions _jwtOptions;
    private readonly IDateTime _dateTime;

    public JwtProvier(IOptions<JwtOptions> jwtOptions, IDateTime dateTime)
    {
        _dateTime = dateTime;
        _jwtOptions = jwtOptions.Value;
    }

    public string Create(AppUser user)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));

        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var claims = new Claim[]
        {
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Email, user.Email!),
            new(JwtRegisteredClaimNames.Name, user.UserName!),
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

        string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
        
        return tokenValue;
    }
}