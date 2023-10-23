using System.ComponentModel.DataAnnotations;

namespace DanilvarKanji.Shared.Models;

public class WordMeaning
{
    [Key]
    public string Id { get; set; }
    public string Definition { get; set; }
    public float? Priority { get; set; }

    public WordMeaning()
    {
        Id = Guid.NewGuid().ToString("N");
    }
}