using System.ComponentModel;
using DanilvarKanji.Shared.Domain.Entities;
using DanilvarKanji.Shared.Domain.Enumerations;

namespace DanilvarKanji.Shared.Requests.Characters;

public class CreateCharacterRequest
{
    [DefaultValue("来")]
    public string Definition { get; set; } = "来";

    public JlptLevel JlptLevel { get; set; }
    public CharacterType CharacterType { get; set; } = CharacterType.Kanji;
    
    public ICollection<StringDefinition>? Mnemonics { get; set; } = new List<StringDefinition>()
    {
        new()
        {
            Value = "There's rice spread out all over the ground. This will cause the Predator to come because he loves kome (rice).",
            Culture = Culture.EnUS
        },
        new()
        {
            Value = "По всей земле рассыпан рис. Это заставит Хищника подойти ведь он любть kome (рис).",
            Culture = Culture.RuRU
        },
    };

    public int? StrokeCount { get; set; } = 7;
    public ICollection<KanjiMeaning> KanjiMeanings { get; set; } = new List<KanjiMeaning>()
    {
        new()
        {
            Definitions = new List<StringDefinition>()
            {
                new("Come", Culture.EnUS),
                new("Подойти", Culture.RuRU),
            },
            Priority = 100
        }
    };
    public List<Kunyomi> Kunyomis { get; set; } = new()
    {
        new()
        {
            JapaneseWriting = "く。る",
        }
    };
    public List<Onyomi> Onyomis { get; set; } = new()
    {
        new()
        {
            JapaneseWriting = "ラ"
        }
    };
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