using DanilvarKanji.Shared.Requests.Auth;
using DanilvarKanji.Shared.Responses.Auth;

namespace DanilvarKanji.Client.Services.Auth;

public interface IAuthService
{
  Task<RegisterUserRequest?> RegisterUserAsync(RegisterUserRequest request);
  Task<LoginResponse?> LoginAsync(LoginUserRequest request);
  Task<string> GetJwtAsync();

  Task<bool> RefreshAsync();
  Task LogoutAsync();
  ValueTask<bool> HasRoleAsync(string role);
  ValueTask<bool> HasAnyOfSpecifiedRolesAsync(IEnumerable<string> roles);
  ValueTask<bool> HasAnyRoleAsync();
  Task ConfirmRegistrationAsync(ConfirmRegistrationRequest request);
}