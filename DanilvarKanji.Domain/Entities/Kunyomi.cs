using Danilvar.Entity;

namespace DanilvarKanji.Domain.Entities;

public class Kunyomi : Entity
{
    public string? Romaji { get; set; }

    public string JapaneseWriting { get; set; }

    public Kunyomi()
    {
        
    }
    
    public Kunyomi(string? romaji, string japaneseWriting)
    {
        Romaji = romaji;
        JapaneseWriting = japaneseWriting;
    }
}