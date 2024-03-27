using DanilvarKanji.Shared.Domain.Entities;
using DanilvarKanji.Shared.Domain.Params;

namespace DanilvarKanji.Domain.RepositoryAbstractions;

public interface IUserRepository
{
    Task<AppUser?> GetByIdAsync(string id);
    Task<AppUser?> GetByEmailAsync(string email);
    Task<IEnumerable<AppUser>> ListAsync(PaginationParams? paginationParams);
    Task<bool> ExistById(string email);
    Task<bool> AnyExist();
    Task<bool> ExistByEmail(string email);
    void Create(AppUser? user);
    Task UpdateUserLearningSettingsAsync(string email, LearningSettings learningSettings);
    Task<LearningSettings?> GetUserLearningSettingsAsync(string email);
    Task UpdateUserXpAsync(int xp, string email);
    ValueTask<bool> AnyExistAsync();
    Task DeleteAsync(string email);
}