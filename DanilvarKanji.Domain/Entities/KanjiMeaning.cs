using System.ComponentModel.DataAnnotations;
using Danilvar.Entity;

namespace DanilvarKanji.Domain.Entities;

public class KanjiMeaning : Entity
{
  
    //public string Definition { get; set; }
    public float? Priority { get; set; }

    public ICollection<StringDefinition>? Definitions { get; set; }
    //public Character? Character { get; set; }
}