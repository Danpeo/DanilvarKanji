namespace DanilvarKanji.Models;

public class Onyomi
{
    public int Id { get; set; }
    public string JapaneseWriting { get; set; }
    public string? Romaji { get; set; }
    public Character Character { get; set; }
}