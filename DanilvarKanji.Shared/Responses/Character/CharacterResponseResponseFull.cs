using DanilvarKanji.Shared.Domain.Entities;

namespace DanilvarKanji.Shared.Responses.Character;

public class CharacterResponseResponseFull : CharacterResponseBase
{
    

    //public ICollection<StringDefinition>? Definitions { get; set; } = new List<StringDefinition>();
    public ICollection<StringDefinition>? Mnemonics { get; set; } = new List<StringDefinition>();
    
    public ICollection<KanjiMeaning>? KanjiMeanings { get; set; } = new List<KanjiMeaning>();
    public ICollection<Kunyomi>? Kunyomis { get; set; } = new List<Kunyomi>();
    public ICollection<Onyomi>? Onyomis { get; set; } = new List<Onyomi>();
    public ICollection<Word>? Words { get; set; }
    
    public List<string>? ChildCharacterIds { get; set; }
   
    public CharacterResponseResponseFull()
    {
        Id = Guid.NewGuid().ToString("N");
    }
}