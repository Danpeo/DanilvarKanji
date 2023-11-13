using DanilvarKanji.Domain.Entities;
using DanilvarKanji.Domain.Enumerations;

namespace DanilvarKanji.Shared.Requests.Characters;

public record CreateCharacterRequest
{
    public string? Definition { get; set; }

    public JlptLevel JlptLevel { get; set; }
    public CharacterType CharacterType { get; set; } = CharacterType.Kanji;
    public ICollection<StringDefinition>? Mnemonics { get; set; } = new List<StringDefinition>();

    public int? StrokeCount { get; set; }
    public ICollection<KanjiMeaning>? KanjiMeanings { get; set; } = new List<KanjiMeaning>();
    public ICollection<Kunyomi>? Kunyomis { get; set; } = new List<Kunyomi>();
    public ICollection<Onyomi>? Onyomis { get; set; } = new List<Onyomi>();
    public ICollection<Word>? Words { get; set; }

    public List<string>? ChildCharacterIds { get; set; }
}