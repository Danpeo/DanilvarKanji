using DanilvarKanji.Shared.Requests.Auth;
using DanilvarKanji.Shared.Responses.Auth;

namespace DanilvarKanji.Client.Services.Auth;

public interface IAuthService
{
    Task<RegisterUserRequest?> RegisterUserAsync(RegisterUserRequest request);
    Task<LoginResponse?> LoginAsync(LoginUserRequest request);
    ValueTask<string> GetJwtAsync();

    event Action<string?>? LoginChange;
    bool IsLoggedIn { get; }
    ValueTask<bool> IsAuthorized();
    Task<bool> RefreshAsync();
    Task LogoutAsync();
}