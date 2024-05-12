using DanilvarKanji.Domain.Shared.Enumerations;
using DanilvarKanji.Shared.Requests.CharacterLearnings;
using DanilvarKanji.Shared.Responses.CharacterLearning;

namespace DanilvarKanji.Client.Services.Characters;

public interface ICharacterLearningApiService
{
  Task<CharacterLearningResponseFull?> CreateCharacterLearningAsync(
    CreateCharacterLearningRequest request
  );

  Task<IEnumerable<CharacterLearningResponseBase?>?> ListReviewQueueAsync(
    int pageNumber = 0,
    int pageSize = 0
  );

  Task<CharacterLearningResponseBase?> GetNextInReviewQueueAsync();
  Task<CharacterLearningResponseFull?> GetLearningAsync(string? id);

  Task<RandomItemsInReviewResponse?> GetRandomMeaningsInReviewAsync(
    string characterId,
    Culture culture,
    int qty = 4
  );

  Task<RandomItemsInReviewResponse?> GetRandomKunReadingsInReviewAsync(
    string characterId,
    int qty = 4
  );

  Task<RandomItemsInReviewResponse?> GetRandomOnReadingsInReviewAsync(
    string characterId,
    int qty = 4
  );

  Task ToggleSkipStateAsync(string? id);

  Task<List<CharacterLearningResponseBase>?> ListSkippedAsync(int pageNumber = 0, int pageSize = 0);

  Task<IEnumerable<CharacterLearningResponseBase?>?> ListFutureReviewQueueAsync(
    int pageNumber = 0,
    int pageSize = 0
  );

  Task<List<CharacterLearningResponseBase>?> ListCompletelyLearnedAsync(
    int pageNumber = 0,
    int pageSize = 0
  );
}