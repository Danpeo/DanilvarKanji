using System.ComponentModel.DataAnnotations;

namespace DanilvarKanji.Models;

public class Onyomi
{
    [Key]
    public Guid Id { get; set; }
    public string JapaneseWriting { get; set; }
    public string? Romaji { get; set; }
}