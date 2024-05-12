using DanilvarKanji.Domain.Shared.Entities;
using DanilvarKanji.Domain.Shared.ValueObjects;

namespace DanilvarKanji.Shared.Responses.Character;

public class CharacterResponseResponseFull : CharacterResponseBase
{
  public CharacterResponseResponseFull()
  {
    Id = Guid.NewGuid().ToString("N");
  }

  //public ICollection<StringDefinition>? Definitions { get; set; } = new List<StringDefinition>();
  public ICollection<StringDefinition>? Mnemonics { get; set; } = new List<StringDefinition>();

  public ICollection<KanjiMeaning>? KanjiMeanings { get; set; } = new List<KanjiMeaning>();
  public ICollection<Kunyomi>? Kunyomis { get; set; } = new List<Kunyomi>();
  public ICollection<Onyomi>? Onyomis { get; set; } = new List<Onyomi>();

  public List<string>? ChildCharacterIds { get; set; }
}