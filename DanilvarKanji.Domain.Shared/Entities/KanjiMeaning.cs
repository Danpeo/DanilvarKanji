using Danilvar.Entity;
using DanilvarKanji.Domain.Shared.ValueObjects;

namespace DanilvarKanji.Domain.Shared.Entities;

public class KanjiMeaning : Entity
{
  public KanjiMeaning()
  {
  }

  public KanjiMeaning(float priority, ICollection<StringDefinition> definitions)
  {
    Priority = priority;
    Definitions = definitions;
  }

  public float? Priority { get; set; }

  public ICollection<StringDefinition>? Definitions { get; set; } = new List<StringDefinition>();
}