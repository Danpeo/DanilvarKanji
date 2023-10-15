using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using DanilvarKanji.Attributes;
using DanilvarKanji.DTO;
using DanilvarKanji.Models.Enums;
using DanilvarKanji.Utils;


namespace DanilvarKanji.Models;

public class Character
{
    [Key]
    public string Id { get; set; }
    public string Definition { get; set; }
    public JlptLevel? JlptLevel { get; set; }
    public CharacterType CharacterType { get; set; }
    public string? Mnemonic { get; set; }
    public int? StrokeCount { get; set; }
    public ICollection<KanjiMeaning>? KanjiMeanings { get; set; }
    public ICollection<Kunyomi>? Kunyomis { get; set; }
    public ICollection<Onyomi>? Onyomis { get; set; }
    public ICollection<Word>? Words { get; set; }

    public Character()
    {
        Id = Guid.NewGuid().ToString("N");
    }

    public Character(CharacterDto dto) : this()
    {
        Definition = dto.Definition;
        JlptLevel = dto.JlptLevel ?? Enums.JlptLevel.N5;
        CharacterType = dto.CharacterType ?? CharacterType.Kanji;
        Mnemonic = dto.Mnemonic;
        StrokeCount = dto.StrokeCount;
        KanjiMeanings = dto.KanjiMeanings;
        Kunyomis = dto.Kunyomis;
        Onyomis = dto.Onyomis;
        Words = dto.Words;
    }
}