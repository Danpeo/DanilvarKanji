using System.ComponentModel.DataAnnotations;
using DanilvarKanji.Models.Enums;

namespace DanilvarKanji.Models;

public class CharacterLearning
{
    [Key]
    public Guid Id { get; set; }
    public AppUser AppUser { get; set; }
    public Character Character { get; set; }
    public LearningState LearningState { get; set; }
    public float LearningProgress { get; set; }
    public int LearnedCount { get; set; }
}