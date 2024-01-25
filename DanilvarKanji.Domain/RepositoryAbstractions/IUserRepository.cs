using DanilvarKanji.Domain.Params;
using DanilvarKanji.Shared.Domain.Entities;

namespace DanilvarKanji.Domain.RepositoryAbstractions;

public interface IUserRepository
{
    Task<AppUser?> GetByIdAsync(string id);
    Task<AppUser> GetByEmailAsync(string email);
    Task<IEnumerable<AppUser>> ListAsync(PaginationParams paginationParams);
    Task<bool> ExistById(string email);
    Task<bool> AnyExist();
    Task<bool> ExistByEmail(string email);
    void Create(AppUser? user);
}