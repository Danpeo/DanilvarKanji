using DanilvarKanji.Shared.Domain.Entities;
using DanilvarKanji.Shared.Responses.Character;

namespace DanilvarKanji.Shared.Responses.CharacterLearning;

public class CharacterLearningResponseFull : CharacterLearningResponseBase
{
    public AppUser AppUser { get; set; }
    public CharacterResponseResponseFull CharacterFull { get; set; }
}