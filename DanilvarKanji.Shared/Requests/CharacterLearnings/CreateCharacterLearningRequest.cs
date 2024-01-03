using DanilvarKanji.Domain.Enumerations;

namespace DanilvarKanji.Shared.Requests.CharacterLearnings;

public class CreateCharacterLearningRequest
{
    public LearningState LearningState { get; set; }
    public int LearnedCount { get; set; } = 0;
    public string CharacterId { get; set; }

    public CreateCharacterLearningRequest()
    {
        
    }

    public CreateCharacterLearningRequest(string characterId, LearningState learningState, int learnedCount = 0)
    {
        CharacterId = characterId;
        LearningState = learningState;
        LearnedCount = learnedCount;
    }
}