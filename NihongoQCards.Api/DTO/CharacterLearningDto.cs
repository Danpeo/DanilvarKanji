using DanilvarKanji.Models.Enums;

namespace DanilvarKanji.DTO;

public class CharacterLearningDto
{
    public int Id { get; set; }
    public float LearningProgress { get; set; }
    public LearningState LearningState { get; set; }
    public int LearnedCount { get; set; } = 0;
    public int CharacterId { get; set; }
}