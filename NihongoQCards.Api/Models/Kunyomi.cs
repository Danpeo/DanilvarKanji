using System.ComponentModel.DataAnnotations;

namespace DanilvarKanji.Models;

public class Kunyomi
{
    [Key]
    public string Id { get; set; }
    public string JapaneseWriting { get; set; }
    public string? Romaji { get; set; }

    public Kunyomi()
    {
        Id = Guid.NewGuid().ToString("N");
    }
}