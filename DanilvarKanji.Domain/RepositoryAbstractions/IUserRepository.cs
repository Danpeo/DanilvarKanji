using DanilvarKanji.Domain.Entities;
using DanilvarKanji.Domain.Shared.Entities;
using DanilvarKanji.Domain.Shared.Params;

namespace DanilvarKanji.Domain.RepositoryAbstractions;

public interface IUserRepository
{
    Task<AppUser?> GetByIdAsync(string id);
    Task<AppUser?> GetByEmailAsync(string email);
    IEnumerable<AppUser> List(PaginationParams paginationParams);
    Task<bool> ExistById(string email);
    Task<bool> AnyExist();
    Task<bool> ExistByEmail(string email);
    Task UpdateUserLearningSettingsAsync(string email, LearningSettings learningSettings);
    Task<LearningSettings?> GetUserLearningSettingsAsync(string email);
    ValueTask<bool> AnyExistAsync();
    Task DeleteAsync(string email);
    Task UpdateUserAsync(string email, string newUserName, string newUserRole);
    void CreateEmailCode(EmailCode emailCode);
    Task<string?> GetRegistrationConfirmationCodeAsync(string email);
    Task DeleteEmailCodeAsync(string email);
}