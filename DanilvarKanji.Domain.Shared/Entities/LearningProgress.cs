using DanilvarKanji.Domain.Shared.Enumerations;

namespace DanilvarKanji.Domain.Shared.Entities;

public class LearningProgress 
{
    public LearningLevel LearningLevel { get; set; }
    public float Value { get; set; }

    public LearningProgress()
    {
        LearningLevel = LearningLevel.L1;
        Value = 0.0f;
    }
}