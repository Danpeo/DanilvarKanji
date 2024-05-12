using DanilvarKanji.Domain.Shared.Enumerations;

namespace DanilvarKanji.Shared.Responses.Character;

public class CharacterResponseBase
{
  public string Id { get; set; }
  public string? Definition { get; set; }
  public JlptLevel JlptLevel { get; set; }
  public CharacterType CharacterType { get; set; }
  public int? StrokeCount { get; set; }

  public string GetCharTypeStr()
  {
    return CharacterType switch
    {
      CharacterType.Kanji => "K",
      CharacterType.Radical => "R",
      _ => "N"
    };
  }
}