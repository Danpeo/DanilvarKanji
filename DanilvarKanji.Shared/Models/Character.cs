using System.ComponentModel.DataAnnotations;
using DanilvarKanji.Shared.Models.Enums;

namespace DanilvarKanji.Shared.Models;

public class Character
{
    [Key]
    public string Id { get; set; }
    public string? Definition { get; set; }
    //public ICollection<StringDefinition>? Definitions { get; set; }
    public JlptLevel JlptLevel { get; set; }
    public CharacterType CharacterType { get; set; }
    public ICollection<StringDefinition>? Mnemonics { get; set; }
    public int? StrokeCount { get; set; }
    public ICollection<KanjiMeaning>? KanjiMeanings { get; set; }
    public ICollection<Kunyomi>? Kunyomis { get; set; }
    public ICollection<Onyomi>? Onyomis { get; set; }
    public ICollection<Word>? Words { get; set; }

    public List<string>? ChildCharacterIds { get; set; }
    
    public Character()
    {
        Id = Guid.NewGuid().ToString("N");
    }
    
}