using Danilvar.Entity;
using DanilvarKanji.Domain.Shared.Enumerations;
using DanilvarKanji.Domain.Shared.ValueObjects;

namespace DanilvarKanji.Domain.Shared.Entities;

public class Character : Entity
{
  public string Definition { get; set; } = "";
  public JlptLevel JlptLevel { get; set; }
  public CharacterType CharacterType { get; set; }
  public ICollection<StringDefinition> Mnemonics { get; set; } = new List<StringDefinition>();
  public int StrokeCount { get; set; }
  public ICollection<KanjiMeaning> KanjiMeanings { get; set; } = new List<KanjiMeaning>();
  public ICollection<Kunyomi>? Kunyomis { get; set; }
  public ICollection<Onyomi>? Onyomis { get; set; }
}