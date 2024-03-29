using DanilvarKanji.Shared.Domain.Entities;
using DanilvarKanji.Shared.Domain.Params;

namespace DanilvarKanji.Domain.RepositoryAbstractions;

public interface IExerciseRepository
{
    void Create(Exercise ex);
    Task<IEnumerable<Exercise>> ListAsync(PaginationParams? paginationParams, AppUser user);
    Task<Exercise?> GetAsync(string id, AppUser user);
    Task<bool> AnyExist();
    Task<bool> AnyToReview(AppUser appUser);
    Task<bool> Exist(string id, AppUser user);
}