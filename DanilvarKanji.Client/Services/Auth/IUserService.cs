using DanilvarKanji.Shared.Domain.Params;
using DanilvarKanji.Shared.Responses.User;

namespace DanilvarKanji.Client.Services.Auth;

public interface IUserService
{
    Task<GetUserResponse?> GetUserAsync();

    Task UpdateLearningSettingsAsync(LearningSettings settings);

    Task<LearningSettings?> GetLearningSettingsAsync();
}