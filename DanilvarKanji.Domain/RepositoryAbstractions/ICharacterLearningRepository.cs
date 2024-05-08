using DanilvarKanji.Domain.Shared.Entities;
using DanilvarKanji.Domain.Shared.Enumerations;
using DanilvarKanji.Domain.Shared.Params;

namespace DanilvarKanji.Domain.RepositoryAbstractions;

public interface ICharacterLearningRepository
{
    Task<IEnumerable<CharacterLearning>> ListLearnQueueAsync(PaginationParams? paginationParams,
        AppUser user,
        JlptLevel jlptLevel = JlptLevel.N5);

    ValueTask<bool> AnyExistAsync();
    void Create(CharacterLearning characterLearning);
    Task<CharacterLearning?> GetAsync(string id, AppUser user);
    ValueTask<bool> Exist(string requestId, AppUser user);

    Task<IEnumerable<CharacterLearning>> ListCurrentReviewQueueAsync(PaginationParams? paginationParams,
        AppUser user);

    ValueTask<bool> AnyToReview(AppUser appUser);
    Task<CharacterLearning?> GetNextInReviewQueue(AppUser appUser);
    Task<List<string>> GetRandomMeaningsInReviewQueueAsync(string characterId, AppUser user, Culture culture, int qty);
    Task<List<string>> GetRandomKunyomisInReviewQueueAsync(string characterId, AppUser user, int qty);
    Task<List<string>> GetRandomOnyomisInReviewQueueAsync(string characterId, AppUser user, int qty);
    Task UpdateProgressAsync(string id, AppUser user, bool lastReviewWasCorrect);
    Task ToggleSkipStateAsync(string id, AppUser user);
    Task<IEnumerable<CharacterLearning>> ListSkippedAsync(PaginationParams? paginationParams, AppUser user);

    Task UpdateProgressOnCharacterAsync(string characterId, AppUser user, DateTime reviewDateTime,
        bool isCorrect);

    float TestLearningSettings(string message);
    Task<CharacterLearning?> GetByCharacterIdAsync(string id, AppUser user);
    void UpdateCharacterLearning(CharacterLearning characterLearning);

    Task<IEnumerable<CharacterLearning>> ListFutureReviewQueueAsync
    (
        PaginationParams? paginationParams,
        AppUser user
    );

    Task<IEnumerable<CharacterLearning>> ListCompletelyLearnedCharactersAsync(
        PaginationParams? paginationParams, AppUser user);
}