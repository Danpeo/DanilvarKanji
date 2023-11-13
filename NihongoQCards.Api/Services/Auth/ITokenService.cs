using DanilvarKanji.Domain.Entities;

namespace DanilvarKanji.Services.Auth;

public interface ITokenService
{
    string CreateToken(AppUser user);
}