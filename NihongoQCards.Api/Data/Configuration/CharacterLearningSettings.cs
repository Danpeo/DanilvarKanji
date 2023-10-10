namespace DanilvarKanji.Data.Configuration;

public record CharacterLearningSettings
{
    public float MaxLearningRate { get; set; }
    public float MinLearningRate { get; set; }
    public int LearnedCountToCompletelyLearn { get; set; }
}