using DanilvarKanji.Domain.Shared.Enumerations;

namespace DanilvarKanji.Domain.Shared.Entities;

public class LearningProgress
{
    public LearningProgress()
    {
        LearningLevel = LearningLevel.L1;
        Value = 0.0f;
    }

    public LearningLevel LearningLevel { get; set; }
    public float Value { get; set; }
}