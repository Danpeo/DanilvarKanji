using DanilvarKanji.Domain.Shared.Entities;
using DanilvarKanji.Shared.Responses.Character;

namespace DanilvarKanji.Shared.Responses.Exercise;

public class ExerciseResponseFull : ExerciseResponseBase
{
    public AppUser AppUser { get; set; }
    public CharacterResponseResponseFull CharacterFull { get; set; }
}