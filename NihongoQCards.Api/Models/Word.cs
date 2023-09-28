using DanilvarKanji.Models.Enums;

namespace DanilvarKanji.Models;

public class Word
{
    public int Id { get; set; }
    public string? Furigana { get; set; }
    public string Romaji { get; set; }
    public string FullJapanese { get; set; }
    public PartOfSpeach PartOfSpeach { get; set; }
    public JlptLevel? JlptLevel { get; set; }
    public ICollection<Meaning> Meanings { get; set; }
}