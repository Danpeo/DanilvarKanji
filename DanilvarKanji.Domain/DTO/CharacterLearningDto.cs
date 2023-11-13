using DanilvarKanji.Domain.Enumerations;

namespace DanilvarKanji.Domain.DTO;

public class CharacterLearningDto
{
    public string Id { get; set; }
    public float LearningProgress { get; set; }
    public LearningState LearningState { get; set; }
    public int LearnedCount { get; set; } = 0;
    public string CharacterId { get; set; }
}