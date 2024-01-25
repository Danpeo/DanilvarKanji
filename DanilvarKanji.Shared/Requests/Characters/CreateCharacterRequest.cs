using DanilvarKanji.Shared.Domain.Entities;
using DanilvarKanji.Shared.Domain.Enumerations;

namespace DanilvarKanji.Shared.Requests.Characters;

public class CreateCharacterRequest
{
    public string? Definition { get; set; }

    public JlptLevel JlptLevel { get; set; }
    public CharacterType CharacterType { get; set; } = CharacterType.Kanji;
    public ICollection<StringDefinition>? Mnemonics { get; set; } = new List<StringDefinition>();

    public int? StrokeCount { get; set; }
    public ICollection<KanjiMeaning> KanjiMeanings { get; set; } = new List<KanjiMeaning>();
    public List<Kunyomi> Kunyomis { get; set; } = new();
    public List<Onyomi> Onyomis { get; set; } = new();
    public ICollection<Word>? Words { get; set; } = new List<Word>();

    public List<string>? ChildCharacterIds { get; set; } = new();

    public JlptLevel FromIntToJlpt(int jlpt)
    {
        return jlpt switch
        {
            5 => JlptLevel.N5,
            4 => JlptLevel.N4,
            3 => JlptLevel.N3,
            2 => JlptLevel.N2,
            1 => JlptLevel.N1,
            _ => JlptLevel.N5
        };
    }
}