namespace DanilvarKanji.Shared.Domain.Settings;

public class CharacterLearningSettings
{
    public float MaxLearningRate { get; set; }
    public float MinLearningRate { get; set; }
    public float PointAfterCorrectExercise { get; set; }
    public int CompletelyLearnedAfter { get; set; }
}
