using Danilvar.Entity;
using DanilvarKanji.Domain.Shared.Enumerations;

namespace DanilvarKanji.Domain.Shared.Entities;

public class CharacterLearning : Entity
{
    public string Id { get; set; }
    public AppUser AppUser { get; set; }
    public Character Character { get; set; }
    public LearningState LearningState { get; set; }
    public LearningLevel LearningLevel { get; set; }
    public float LearningLevelValue { get; set; }
    public int LearnedCount { get; set; }
    public DateTime LastReviewDateTime { get; set; } = DateTime.UtcNow;
    public DateTime NextReviewDateTime { get; set; } = DateTime.UtcNow;
    public bool LastReviewWasCorrect { get; set; }
}