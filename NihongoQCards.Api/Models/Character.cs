using System.Text.Json.Serialization;
using DanilvarKanji.Attributes;
using DanilvarKanji.DTO;
using DanilvarKanji.Models.Enums;
using DanilvarKanji.Utils;


namespace DanilvarKanji.Models;

public class Character
{
    public int Id { get; set; }
    public string Definition { get; set; }
    public JlptLevel? JlptLevel { get; set; }
    public CharacterType CharacterType { get; set; }
    public string? Mnemonic { get; set; }
    
    [SerializeAsNumericString]
    public int? StrokeCount { get; set; }
    public ICollection<KanjiMeaning>? KanjiMeanings { get; set; }
    public ICollection<Kunyomi>? Kunyomis { get; set; }
    public ICollection<Onyomi>? Onyomis { get; set; }
    public ICollection<Word>? Words { get; set; }

    public Character()
    {
    }

    public Character(CharacterDto dto)
    {
        Id = dto.Id;
        Definition = dto.Definition;
        CharacterType = dto.CharacterType;
        Mnemonic = dto.Mnemonic;
        StrokeCount = dto.StrokeCount;
        KanjiMeanings = dto.KanjiMeanings;
        Kunyomis = dto.Kunyomis;
        Onyomis = dto.Onyomis;
        Words = dto.Words;
    }
}