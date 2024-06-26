using System.ComponentModel.DataAnnotations;
using DanilvarKanji.Domain.Shared.Enumerations;
using DanilvarKanji.Domain.Shared.ValueObjects;

namespace DanilvarKanji.Domain.Shared.Entities;

public class Word
{
    public Word()
    {
        Id = Guid.NewGuid().ToString("N");
    }

    [Key] public string Id { get; set; }

    public string? Furigana { get; set; }
    public string Romaji { get; set; }
    public string FullJapanese { get; set; }
    public PartOfSpeach PartOfSpeach { get; set; }
    public ICollection<StringDefinition> WordMeanings { get; set; }
}