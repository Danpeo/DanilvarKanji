using DanilvarKanji.Shared.Models;
using DanilvarKanji.Shared.Models.Enums;

namespace DanilvarKanji.Shared.DTO;

public class CharacterDto
{
    public string Id { get; set; }
    public string? Definition { get; set; }

    //public ICollection<StringDefinition>? Definitions { get; set; } = new List<StringDefinition>();
    public JlptLevel JlptLevel { get; set; }
    public CharacterType CharacterType { get; set; }
    public ICollection<StringDefinition>? Mnemonics { get; set; } = new List<StringDefinition>();
    
    public int? StrokeCount { get; set; }
    public ICollection<KanjiMeaning>? KanjiMeanings { get; set; }
    public ICollection<Kunyomi>? Kunyomis { get; set; }
    public ICollection<Onyomi>? Onyomis { get; set; }
    public ICollection<Word>? Words { get; set; }

    public CharacterDto()
    {
        Id = Guid.NewGuid().ToString("N");
    }
}