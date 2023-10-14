using DanilvarKanji.Attributes;
using DanilvarKanji.Models;
using DanilvarKanji.Models.Enums;

namespace DanilvarKanji.DTO;

public class CharacterDto
{
    public string Id { get; set; }
    public string? Definition { get; set; }
    public JlptLevel? JlptLevel { get; set; }
    public CharacterType? CharacterType { get; set; }
    public string? Mnemonic { get; set; }
    
    public int? StrokeCount { get; set; }

    public string TEST { get; set; }
    public ICollection<KanjiMeaning>? KanjiMeanings { get; set; }
    public ICollection<Kunyomi>? Kunyomis { get; set; }
    public ICollection<Onyomi>? Onyomis { get; set; }
    public ICollection<Word>? Words { get; set; }

    public CharacterDto()
    {
        Id = Guid.NewGuid().ToString("N");
    }
    
}