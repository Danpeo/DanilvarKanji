using DanilvarKanji.Shared.Requests.Auth;
using DanilvarKanji.Shared.Responses.Auth;

namespace DanilvarKanji.Client.Services.Auth;

public interface IAuthService
{
    Task<RegisterUserRequest?> RegisterUserAsync(RegisterUserRequest request);
    Task<LoginResponse?> LoginAsync(LoginUserRequest request);
    ValueTask<string> GetJwtAsync();

    Task<bool> RefreshAsync();
    Task LogoutAsync();
    Task<bool> HasRoleAsync(string role);
    Task<bool> HasAnyOfSpecifiedRolesAsync(IEnumerable<string> roles);
    Task<bool> HasAnyRoleAsync();
}