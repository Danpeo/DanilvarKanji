using System.ComponentModel.DataAnnotations;

namespace DanilvarKanji.Shared.Models.Common;

public class Reading
{
    [Key]
    public string Id { get; set; }
    public string JapaneseWriting { get; set; }
    public string? Romaji { get; set; }

    public Reading()
    {
        Id = Guid.NewGuid().ToString("N");
    }
}