using System.ComponentModel.DataAnnotations;

namespace DanilvarKanji.Models;

public class Onyomi
{
    [Key]
    public string Id { get; set; }
    public string JapaneseWriting { get; set; }
    public string? Romaji { get; set; }

    public Onyomi()
    {
        Id = Guid.NewGuid().ToString("N");
    }
}