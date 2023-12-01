using DanilvarKanji.Shared.Responses.User;

namespace DanilvarKanji.Client.Services.Auth;

public interface IUserService
{
    Task<GetUserResponse?> GetUserAsync();
}