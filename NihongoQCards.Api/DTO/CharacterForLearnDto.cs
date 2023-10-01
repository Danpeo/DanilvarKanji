using DanilvarKanji.Models.Enums;

namespace DanilvarKanji.DTO;

public class CharacterForLearnDto
{
    public int Id { get; set; }
    public LearningState LearningState { get; set; }
}