using DanilvarKanji.Shared.Domain.Enumerations;

namespace DanilvarKanji.Shared.Requests.Exercises;

public class CreateExerciseRequest
{
    public string CharacterId { get; set; }
    public bool IsCorrect { get; set; }
    public ExerciseType ExerciseType { get; set; } = ExerciseType.Point;
    public ExerciseSubject ExerciseSubject { get; set; }

    public CreateExerciseRequest(string characterId, bool isCorrect, ExerciseType exerciseType, ExerciseSubject exerciseSubject)
    {
        CharacterId = characterId;
        IsCorrect = isCorrect;
        ExerciseType = exerciseType;
        ExerciseSubject = exerciseSubject;
    }
}