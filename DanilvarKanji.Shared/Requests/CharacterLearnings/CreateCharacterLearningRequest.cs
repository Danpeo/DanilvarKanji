using DanilvarKanji.Domain.Enumerations;

namespace DanilvarKanji.Shared.Requests.CharacterLearnings;

public class CreateCharacterLearningRequest
{
    public LearningState LearningState { get; set; }
    public int LearnedCount { get; set; } = 0;
    public required string CharacterId { get; set; }
}