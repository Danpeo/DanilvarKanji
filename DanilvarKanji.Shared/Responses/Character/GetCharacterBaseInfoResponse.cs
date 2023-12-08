using DanilvarKanji.Domain.Enumerations;

namespace DanilvarKanji.Shared.Responses.Character;

public class GetCharacterBaseInfoResponse
{
    public string Id { get; set; }
    public string? Definition { get; set; }
    public JlptLevel JlptLevel { get; set; }
    public CharacterType CharacterType { get; set; }
    public int? StrokeCount { get; set; }
}