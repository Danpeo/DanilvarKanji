using DanilvarKanji.Models;

namespace DanilvarKanji.Services.Auth;

public interface ITokenService
{
    string CreateToken(AppUser user);
}