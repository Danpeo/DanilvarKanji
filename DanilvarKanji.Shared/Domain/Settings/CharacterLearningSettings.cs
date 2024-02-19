namespace DanilvarKanji.Shared.Domain.Settings;

public class CharacterLearningSettings
{
    public float MaxLearningRate { get; init; }
    public float MinLearningRate { get; init; }
    public float PointAfterCorrectExercise { get; init; }
    public int CompletelyLearnedAfter { get; init; }
    public int ShiftExerciseDateAfterFailInMinutes { get; init; } 
    public float InitRepeatingShiftHrs { get; init; }
    public float NextShiftModifier { get; init; }
    public int MinXp { get; init; }
    public int NormalXp { get; init; }
}
