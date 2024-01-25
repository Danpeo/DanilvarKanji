using DanilvarKanji.Shared.Domain.Entities;
using DanilvarKanji.Shared.Domain.Enumerations;
using DanilvarKanji.Shared.Responses.Character;

namespace DanilvarKanji.Shared.Responses.CharacterLearning;

public class GetCharacterLearningBaseInfoResponse
{
    public string Id { get; set; }
    public GetCharacterBaseInfoResponse Character { get; set; }
    public LearningState LearningState { get; set; }
    public LearningProgress LearningProgress { get; set; }
    public int LearnedCount { get; set; }
    public DateTime LastReviewDateTime { get; set; } = DateTime.Now;
}