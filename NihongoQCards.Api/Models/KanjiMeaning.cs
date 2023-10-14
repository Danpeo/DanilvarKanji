using System.ComponentModel.DataAnnotations;

namespace DanilvarKanji.Models;

public class KanjiMeaning
{
    [Key]
    public string Id { get; set; }
    public string Definition { get; set; }
    public float? Priority { get; set; }
    //public Character? Character { get; set; }

    public KanjiMeaning()
    {
        Id = Guid.NewGuid().ToString("N");
    }
}