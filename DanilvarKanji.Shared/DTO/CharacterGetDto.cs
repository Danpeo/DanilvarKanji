using DanilvarKanji.Shared.Models;
using DanilvarKanji.Shared.Models.Enums;

namespace DanilvarKanji.Shared.DTO;

public class CharacterGetDto
{
    public string Id { get; set; }
    public string? Definition { get; set; }
    public string JlptLevel { get; set; }
    public string CharacterType { get; set; }
    public string? Mnemonic { get; set; }

    public int? StrokeCount { get; set; }

    public string TEST { get; set; }
    public ICollection<KanjiMeaning>? KanjiMeanings { get; set; }
    public ICollection<Kunyomi>? Kunyomis { get; set; }
    public ICollection<Onyomi>? Onyomis { get; set; }
    public ICollection<Word>? Words { get; set; }

    public CharacterGetDto()
    {
        Id = Guid.NewGuid().ToString("N");
    }
}