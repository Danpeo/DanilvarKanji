using DanilvarKanji.Models.Enums;

namespace DanilvarKanji.DTO;

public class CharacterLearningDto
{
    public Guid Id { get; set; }
    public float LearningProgress { get; set; }
    public LearningState LearningState { get; set; }
    public int LearnedCount { get; set; } = 0;
    public Guid CharacterId { get; set; }
}