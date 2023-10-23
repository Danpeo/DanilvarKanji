using System.ComponentModel.DataAnnotations;
using DanilvarKanji.Shared.Models.Enums;

namespace DanilvarKanji.Shared.Models;

public class CharacterLearning
{
    [Key]
    public string Id { get; set; }
    public AppUser AppUser { get; set; }
    public Character Character { get; set; }
    public LearningState LearningState { get; set; }
    //public float LearningProgress { get; set; }
    public LearningProgress LearningProgress { get; set; }
    public int LearnedCount { get; set; }

    public CharacterLearning()
    {
        Id = Guid.NewGuid().ToString("N");
    }
}