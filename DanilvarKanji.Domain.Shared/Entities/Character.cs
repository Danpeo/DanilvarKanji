using Danilvar.Entity;
using DanilvarKanji.Domain.Shared.Enumerations;

namespace DanilvarKanji.Domain.Shared.Entities;

public class Character : Entity
{
    public string? Definition { get; set; }
    //public ICollection<StringDefinition>? Definitions { get; set; }
    public JlptLevel JlptLevel { get; set; }
    public CharacterType CharacterType { get; set; }
    public ICollection<StringDefinition>? Mnemonics { get; set; }
    public int? StrokeCount { get; set; }
    public ICollection<KanjiMeaning>? KanjiMeanings { get; set; }
    public ICollection<Kunyomi>? Kunyomis { get; set; }
    public ICollection<Onyomi>? Onyomis { get; set; }
}