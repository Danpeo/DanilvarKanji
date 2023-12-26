using Danilvar.Entity;
using DanilvarKanji.Domain.Enumerations;

namespace DanilvarKanji.Domain.Entities;

public class CharacterLearning : Entity
{
    public AppUser AppUser { get; set; }
    public Character Character { get; set; }
    public LearningState LearningState { get; set; }
    //public float LearningProgress { get; set; }
    public LearningProgress LearningProgress { get; set; }
    public int LearnedCount { get; set; }
    public DateTime LastReviewDateTime { get; set; } = DateTime.Now;
}