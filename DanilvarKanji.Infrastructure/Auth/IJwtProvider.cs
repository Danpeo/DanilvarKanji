using System.IdentityModel.Tokens.Jwt;
using DanilvarKanji.Domain.Entities;

namespace DanilvarKanji.Infrastructure.Auth;

public interface IJwtProvider
{
    JwtSecurityToken GenerateJwt(AppUser user);
    string GenerateRefreshToken();
    string GetTokenValue(JwtSecurityToken token);
}