using Danilvar.Entity;
using WanaKanaNet;

namespace DanilvarKanji.Domain.Shared.Entities;

public class Onyomi : Entity
{
    private string _japaneseWriting = string.Empty;
    public string Romaji { get; private set; }

    public string JapaneseWriting
    {
        get => _japaneseWriting;
        set
        {
            _japaneseWriting = value;
            _japaneseWriting = WanaKana.ToKatakana(_japaneseWriting);
            Romaji = WanaKana.ToRomaji(_japaneseWriting);
        }
    }

    public Onyomi()
    {
        Romaji = WanaKana.ToRomaji(_japaneseWriting!);
    }

    public Onyomi(string japaneseWriting) : this()
    {
        JapaneseWriting = japaneseWriting;
    }
}