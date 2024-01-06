using DanilvarKanji.Domain.Entities;
using DanilvarKanji.Domain.Enumerations;
using DanilvarKanji.Shared.Requests.CharacterLearnings;
using DanilvarKanji.Shared.Responses.CharacterLearning;

namespace DanilvarKanji.Client.Services.Characters;

public interface ICharacterLearningService
{
    Task<GetAllFromCharacterLearningResponse?> CreateCharacterLearningAsync(CreateCharacterLearningRequest request);

    Task<IEnumerable<GetCharacterLearningBaseInfoResponse?>?> ListReviewQueueAsync(int pageNumber = 0,
        int pageSize = 0);

    Task<GetCharacterLearningBaseInfoResponse?> GetNextInReviewQueueAsync();
    Task<GetAllFromCharacterLearningResponse?> GetLearnignAsync(string? id);
    Task<GetRandomMeaningsInReviewResponse?> GetRandomMeaningsInReviewAsync(string characterId, Culture culture,
        int qty = 4);
}