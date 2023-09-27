using NihongoQCards.Models;

namespace NihongoQCards.DTO;

public class CharacterDto
{
    public int Id { get; set; }
    public string Definition { get; set; }
    public JlptLevel? JlptLevel { get; set; }
    public CharacterType CharacterType { get; set; }
    public string? Mnemonic { get; set; }
    public ICollection<Meaning> Meanings { get; set; }
    public ICollection<string>? Kunyomis { get; set; }
    public ICollection<string>? Onyomis { get; set; }
    public ICollection<Word>? SampleWords { get; set; }
}