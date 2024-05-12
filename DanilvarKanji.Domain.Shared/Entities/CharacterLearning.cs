using Danilvar.Entity;
using DanilvarKanji.Domain.Shared.Enumerations;

namespace DanilvarKanji.Domain.Shared.Entities;

public class CharacterLearning : Entity
{
  //public string Id { get; set; }
  public required AppUser AppUser { get; init; }
  public required Character Character { get; init; }
  public LearningState LearningState { get; set; }
  public LearningLevel LearningLevel { get; init; }
  public float LearningLevelValue { get; set; }
  public int LearnedCount { get; set; }
  public DateTime LastReviewDateTime { get; set; } = DateTime.UtcNow;
  public DateTime NextReviewDateTime { get; set; } = DateTime.MinValue;
  public bool LastReviewWasCorrect { get; set; }
}