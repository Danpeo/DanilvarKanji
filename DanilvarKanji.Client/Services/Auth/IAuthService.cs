using DanilvarKanji.Shared.Requests.Auth;

namespace DanilvarKanji.Client.Services.Auth;

public interface IAuthService
{
    Task<RegisterUserRequest?> RegisterUserAsync(RegisterUserRequest user);
}