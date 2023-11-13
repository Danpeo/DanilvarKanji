using DanilvarKanji.Domain.Enumerations;

namespace DanilvarKanji.Domain.Entities;

public class LearningProgress
{
    public string Id { get; set; }
    public LearningLevel LearningLevel { get; set; }
    public float Value { get; set; }

    public LearningProgress()
    {
        Id = Guid.NewGuid().ToString("N");
        LearningLevel = LearningLevel.L1;
        Value = 0.0f;
    }
}