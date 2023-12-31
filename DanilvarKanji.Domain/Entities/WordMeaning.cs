using System.ComponentModel.DataAnnotations;

namespace DanilvarKanji.Domain.Entities;

public class WordMeaning
{
    [Key]
    public string Id { get; set; }
    //public string Definition { get; set; }
    public ICollection<StringDefinition> Definitions { get; set; }
    public float? Priority { get; set; }

    public WordMeaning()
    {
        Id = Guid.NewGuid().ToString("N");
    }
}