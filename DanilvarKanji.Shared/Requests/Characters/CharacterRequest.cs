using System.ComponentModel;
using DanilvarKanji.Domain.Shared.Entities;
using DanilvarKanji.Domain.Shared.Enumerations;
using DanilvarKanji.Domain.Shared.ValueObjects;

namespace DanilvarKanji.Shared.Requests.Characters;

public class CharacterRequest
{
    [DefaultValue("来")] public string Definition { get; set; } = "来";

    public JlptLevel JlptLevel { get; set; } = JlptLevel.N5;
    public CharacterType CharacterType { get; set; } = CharacterType.Kanji;

    public ICollection<StringDefinition> Mnemonics { get; set; } = new List<StringDefinition>();


    public int? StrokeCount { get; set; } = 7;
    public ICollection<KanjiMeaning> KanjiMeanings { get; set; } = new List<KanjiMeaning>();

    public List<Kunyomi> Kunyomis { get; set; } = new();

    public List<Onyomi> Onyomis { get; set; } = new();

    /*
    public ICollection<Word>? Words { get; set; } = new List<Word>();
    */

    /*
    public List<string>? ChildCharacterIds { get; set; } = new();
    */

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

    public void Init()
    {
        Mnemonics.Add(new StringDefinition
        {
            Value =
                "There's rice spread out all over the ground. This will cause the Predator to come because he loves kome (rice).",
            Culture = Culture.EnUS
        });
        Mnemonics.Add(new StringDefinition
        {
            Value = "По всей земле рассыпан рис. Это заставит Хищника подойти ведь он любть kome (рис).",
            Culture = Culture.RuRU
        });

        KanjiMeanings.Add(new KanjiMeaning
        {
            Definitions = new List<StringDefinition>()
            {
                new("Come", Culture.EnUS),
                new("Подойти", Culture.RuRU),
            },
            Priority = 100
        });

        Kunyomis.Add(new Kunyomi
        {
            JapaneseWriting = "く。る",
        });

        Onyomis.Add(new Onyomi
        {
            JapaneseWriting = "ラ"
        });
    }
}