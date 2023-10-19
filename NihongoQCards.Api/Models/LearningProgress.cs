using DanilvarKanji.Models.Enums;

namespace DanilvarKanji.Models;

public class LearningProgress
{
    public string Id { get; set; }
    public LearningLevel LearningLevel { get; set; }
    public float Value { get; set; }

    public LearningProgress()
    {
        Id = Guid.NewGuid().ToString("N");
        LearningLevel = LearningLevel.Novice;
        Value = 0.0f;
    }
}