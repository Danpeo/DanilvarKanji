using DanilvarKanji.DTO;
using DanilvarKanji.Models.Common;
using DanilvarKanji.Models.Enums;

namespace DanilvarKanji.Models;

public class Character
{
    public int Id { get; set; }
    public string Definition { get; set; }
    public JlptLevel? JlptLevel { get; set; }
    public CharacterType CharacterType { get; set; }
    public string? Mnemonic { get; set; }
    public ICollection<KanjiMeaning> KanjiMeanings { get; set; }
    public List<Kunyomi>? Kunyomis { get; set; }
    public List<Onyomi>? Onyomis { get; set; }
    public ICollection<Word>? SampleWords { get; set; }

    public Character()
    {
        
    }

    public Character(CharacterDto dto)
    {
        Id = dto.Id;
        Definition = dto.Definition;
        CharacterType = dto.CharacterType;
        Mnemonic = dto.Mnemonic;
        KanjiMeanings = dto.KanjiMeanings;
        Kunyomis = dto.Kunyomis;
        Onyomis = dto.Onyomis;
        SampleWords = dto.SampleWords;
    }
}