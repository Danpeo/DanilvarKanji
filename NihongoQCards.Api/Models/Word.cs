using System.ComponentModel.DataAnnotations;
using DanilvarKanji.Models.Enums;

namespace DanilvarKanji.Models;

public class Word
{
    [Key]
    public string Id { get; set; }
    public string? Furigana { get; set; }
    public string Romaji { get; set; }
    public string FullJapanese { get; set; }
    public PartOfSpeach PartOfSpeach { get; set; }
    public ICollection<WordMeaning> WordMeanings { get; set; }

    public Word()
    {
        Id = Guid.NewGuid().ToString("N");
    }
}