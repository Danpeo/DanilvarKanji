using DanilvarKanji.Domain.Shared.Entities;
using DanilvarKanji.Domain.Shared.Params;
using DanilvarKanji.Shared.Requests.Users;
using DanilvarKanji.Shared.Responses.User;

namespace DanilvarKanji.Client.Services.Auth;

public interface IUserService
{
    Task<UserResponseBase?> GetUserAsync();

    Task UpdateLearningSettingsAsync(LearningSettings settings);

    Task<LearningSettings?> GetLearningSettingsAsync();

    Task<IEnumerable<AppUser>?> ListUsersAsync(int pageNumber = 0,
        int pageSize = 0);

    Task DeleteUserAsync(string email);
    Task UpdateUserAsync(string userEmail, UpdateUserRequest request);
}