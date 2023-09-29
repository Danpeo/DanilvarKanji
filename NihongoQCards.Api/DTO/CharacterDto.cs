using DanilvarKanji.Models;
using DanilvarKanji.Models.Enums;

namespace DanilvarKanji.DTO;

public class CharacterDto
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
}