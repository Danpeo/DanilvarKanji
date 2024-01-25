
using DanilvarKanji.Shared.Domain.Entities;

namespace DanilvarKanji.Domain.RepositoryAbstractions;

public interface IReviewRepository
{
    void Create(ReviewSession session);
    Task<ReviewSession?> GetAsync(string id, AppUser user);
    ValueTask<bool> ExistAsync(string id, AppUser user);
}