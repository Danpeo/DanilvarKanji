using DanilvarKanji.Shared.Domain.Enumerations;
using DanilvarKanji.Shared.Responses.Character;

namespace DanilvarKanji.Shared.Responses.Exercise;

public class ExerciseResponseBase
{
    public string Id { get; set; }
    public CharacterResponseBase CharacterResponse { get; set; }
    public bool IsCorrect { get; set; }
    public ExerciseType ExerciseType { get; set; }
    public ExerciseSubject ExerciseSubject { get; set; }
    public DateTime ExcerciseDateTime { get; set; }

   
}