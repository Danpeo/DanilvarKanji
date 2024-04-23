using DanilvarKanji.Domain.Shared.Enumerations;
using DanilvarKanji.Shared.Responses.Character;

namespace DanilvarKanji.Shared.Responses.CharacterLearning;

public class CharacterLearningResponseBase
{
    public string Id { get; set; }
    public CharacterResponseBase Character { get; set; }
    public LearningState LearningState { get; set; }
    /*
    public LearningProgress LearningProgress { get; set; }
    */
    public LearningLevel LearningLevel { get; set; }
    public float LearningLevelValue { get; set; }
    public int LearnedCount { get; set; }
    public DateTime LastReviewDateTime { get; set; } = DateTime.Now;
    public DateTime NextReviewDateTime { get; set; }
}