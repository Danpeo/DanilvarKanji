using DanilvarKanji.Shared.Domain.Enumerations;

namespace DanilvarKanji.Shared.Domain.Entities;

public class CharacterLearning 
{
    public string Id { get; set; }
    public AppUser AppUser { get; set; }
    public Character Character { get; set; }
    public LearningState LearningState { get; set; }
    //public float LearningProgress { get; set; }
    public LearningProgress LearningProgress { get; set; }
    public int LearnedCount { get; set; }
    public DateTime LastReviewDateTime { get; set; } = DateTime.UtcNow;
    public bool LastReviewWasCorrect { get; set; }
}