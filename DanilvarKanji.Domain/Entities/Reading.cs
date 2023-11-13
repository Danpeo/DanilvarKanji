using Danilvar.Entity;
using WanaKanaNet;

namespace DanilvarKanji.Domain.Entities;

public class Reading : Entity
{
    private string _japaneseWriting = string.Empty;
    
    public virtual string JapaneseWriting
    {
        get => _japaneseWriting;
        set => _japaneseWriting = WanaKana.ToKana(value);
    }

    public string? Romaji { get; set; }

}