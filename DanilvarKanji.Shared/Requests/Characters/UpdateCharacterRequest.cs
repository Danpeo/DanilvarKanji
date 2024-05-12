using DanilvarKanji.Domain.Shared.Entities;
using DanilvarKanji.Domain.Shared.Enumerations;
using DanilvarKanji.Domain.Shared.ValueObjects;

namespace DanilvarKanji.Shared.Requests.Characters;

public class UpdateCharacterRequest
{
  public string Definition { get; set; } = "";

  public JlptLevel JlptLevel { get; init; }
  public CharacterType CharacterType { get; init; } = CharacterType.Kanji;
  public ICollection<StringDefinition> Mnemonics { get; set; } = new List<StringDefinition>();

  public int? StrokeCount { get; set; }
  public ICollection<KanjiMeaning> KanjiMeanings { get; set; } = new List<KanjiMeaning>();
  public ICollection<Kunyomi>? Kunyomis { get; set; } = new List<Kunyomi>();
  public ICollection<Onyomi>? Onyomis { get; set; } = new List<Onyomi>();
}