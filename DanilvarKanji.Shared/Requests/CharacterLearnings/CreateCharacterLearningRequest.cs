using DanilvarKanji.Domain.Shared.Enumerations;

namespace DanilvarKanji.Shared.Requests.CharacterLearnings;

public class CreateCharacterLearningRequest
{
    public string Id { get; }
    public LearningState LearningState { get; set; }
    public int LearnedCount { get; set; }
    public string CharacterId { get; set; } = string.Empty;

    public CreateCharacterLearningRequest()
    {
        Id = Guid.NewGuid().ToString("N");
    }

    public CreateCharacterLearningRequest(string characterId, LearningState learningState, int learnedCount = 0) :
        this()
    {
        CharacterId = characterId;
        LearningState = learningState;
        LearnedCount = learnedCount;
    }
}