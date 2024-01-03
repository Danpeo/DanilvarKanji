using DanilvarKanji.Domain.Enumerations;

namespace DanilvarKanji.Shared.Requests.Exercises;

public class CreateExerciseRequest
{
    public string CharacterId { get; set; }
    public bool IsCorrect { get; set; }
    public ReviewType ReviewType { get; set; } = ReviewType.Point;
    public ExerciseType ExerciseType { get; set; }

    public CreateExerciseRequest(string characterId, bool isCorrect, ReviewType reviewType, ExerciseType exerciseType)
    {
        CharacterId = characterId;
        IsCorrect = isCorrect;
        ReviewType = reviewType;
        ExerciseType = exerciseType;
    }
}