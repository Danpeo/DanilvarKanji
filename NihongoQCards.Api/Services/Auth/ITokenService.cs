using DanilvarKanji.Shared.Models;

namespace DanilvarKanji.Services.Auth;

public interface ITokenService
{
    string CreateToken(AppUser user);
}