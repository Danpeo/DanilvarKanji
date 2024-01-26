using DanilvarKanji.Shared.Domain.Entities;
using DanilvarKanji.Shared.Domain.Enumerations;
using DanilvarKanji.Shared.Domain.Params;

namespace DanilvarKanji.Domain.RepositoryAbstractions;

public interface ICharacterLearningRepository
{
    Task<IEnumerable<CharacterLearning>> ListLearnQueueAsync(PaginationParams? paginationParams,
        AppUser user,
        JlptLevel jlptLevel = JlptLevel.N5);

    Task<bool> AnyExist();
    void Create(CharacterLearning characterLearning);
    Task<CharacterLearning?> GetAsync(string id, AppUser user);
    Task<bool> Exist(string requestId, AppUser user);

    Task<IEnumerable<CharacterLearning>> ListReviewQueueAsync(PaginationParams? paginationParams,
        AppUser user);

    Task<bool> AnyToReview(AppUser appUser);
    Task<CharacterLearning?> GetNextInReviewQueue(AppUser appUser);
    Task<List<string>> GetRandomMeaningsInReviewQueueAsync(string characterId, AppUser user, Culture culture, int qty);
    Task<List<string>> GetRandomKunyomisInReviewQueueAsync(string characterId, AppUser user, int qty);
    Task<List<string>> GetRandomOnyomisInReviewQueueAsync(string characterId, AppUser user, int qty);
    Task UpdateProgressAsync(string id, AppUser user, bool lastReviewWasCorrect);
}