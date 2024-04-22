using Danilvar.Entity;
using DanilvarKanji.Shared.Domain.Enumerations;

namespace DanilvarKanji.Shared.Domain.Entities;

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