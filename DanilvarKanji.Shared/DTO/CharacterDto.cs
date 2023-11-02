using DanilvarKanji.Shared.Models;
using DanilvarKanji.Shared.Models.Enums;

namespace DanilvarKanji.Shared.DTO;

public class CharacterDto
{
    public string Id { get; set; }
    public string? Definition { get; set; }

    //public ICollection<StringDefinition>? Definitions { get; set; } = new List<StringDefinition>();
    public JlptLevel JlptLevel { get; set; }
    public CharacterType CharacterType { get; set; } = CharacterType.Kanji;
    public ICollection<StringDefinition>? Mnemonics { get; set; } = new List<StringDefinition>();
    
    public int? StrokeCount { get; set; }
    public ICollection<KanjiMeaning>? KanjiMeanings { get; set; }
    public ICollection<Kunyomi>? Kunyomis { get; set; } = new List<Kunyomi>();
    public ICollection<Onyomi>? Onyomis { get; set; } = new List<Onyomi>();
    public ICollection<Word>? Words { get; set; }
    
    public List<string>? ChildCharacterIds { get; set; }
   
    public CharacterDto()
    {
        Id = Guid.NewGuid().ToString("N");
    }
}