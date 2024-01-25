using DanilvarKanji.Shared.Domain.Enumerations;
using DanilvarKanji.Shared.Responses.Character;

namespace DanilvarKanji.Shared.Responses.Exercise;

public class GetBaseExerciseInfoResponse
{
    public string Id { get; set; }
    public GetCharacterBaseInfoResponse Character { get; set; }
    public bool IsCorrect { get; set; }
    public ReviewType ReviewType { get; set; }
    public ExerciseType ExerciseType { get; set; }
    public DateTime ExcerciseDateTime { get; set; }

   
}