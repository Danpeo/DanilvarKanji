using DanilvarKanji.Shared.Domain.Entities;
using DanilvarKanji.Shared.Responses.Character;

namespace DanilvarKanji.Shared.Responses.Exercise;

public class GetAllFromExerciseResponse : GetBaseExerciseInfoResponse
{
    public AppUser AppUser { get; set; }
    public GetAllFromCharacterResponse CharacterFull { get; set; }
}