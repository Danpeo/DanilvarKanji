using DanilvarKanji.Models.Enums;

namespace DanilvarKanji.DTO;

public class CharacterForLearnDto
{
    public int Id { get; set; }
    public float LearningProgress { get; set; }
    public LearningState LearningState { get; set; }
}