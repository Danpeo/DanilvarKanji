using Danilvar.Entity;

namespace DanilvarKanji.Domain.Shared.Entities;

public class KanjiMeaning : Entity
{
  
    //public string Definition { get; set; }
    public float? Priority { get; set; }

    public ICollection<StringDefinition>? Definitions { get; set; } = new List<StringDefinition>();
    //public Character? Character { get; set; }

    public KanjiMeaning()
    {
        
    }

    public KanjiMeaning(float priority, ICollection<StringDefinition> definitions)
    {
        Priority = priority;
        Definitions = definitions;
    }
}