using Danilvar.Entity;

namespace DanilvarKanji.Domain.Entities;

public class KanjiMeaning : Entity
{
  
    //public string Definition { get; set; }
    public float? Priority { get; set; }

    public ICollection<StringDefinition>? Definitions { get; set; } = new List<StringDefinition>();
    //public Character? Character { get; set; }

    public KanjiMeaning()
    {
        
    }

    public KanjiMeaning(ICollection<StringDefinition> definitions)
    {
        Definitions = definitions;
    }
}