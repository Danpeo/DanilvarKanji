using DanilvarKanji.Models;
using DanilvarKanji.Models.Enums;

namespace DanilvarKanji.DTO;

public class CharacterLearningDto
{
    public int Id { get; set; }
    public AppUser AppUser { get; set; }
    public Character Character { get; set; }
    public LearningState LearningState { get; set; }
    public float LearningProgress { get; set; }
}