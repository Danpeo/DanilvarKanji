using DanilvarKanji.Shared.Domain.Entities;
using DanilvarKanji.Shared.Responses.Character;

namespace DanilvarKanji.Shared.Responses.CharacterLearning;

public class GetAllFromCharacterLearningResponse : GetCharacterLearningBaseInfoResponse
{
    public AppUser AppUser { get; set; }
    public GetAllFromCharacterResponse CharacterFull { get; set; }
}