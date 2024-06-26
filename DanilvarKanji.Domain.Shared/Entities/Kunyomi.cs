using Danilvar.Entity;
using WanaKanaNet;

namespace DanilvarKanji.Domain.Shared.Entities;

public class Kunyomi : Entity
{
    private string _japaneseWriting = string.Empty;

    public Kunyomi()
    {
        Romaji = WanaKana.ToRomaji(_japaneseWriting!);
    }

    public Kunyomi(string japaneseWriting)
        : this()
    {
        JapaneseWriting = japaneseWriting;
    }

    public string Romaji { get; private set; }

    public string JapaneseWriting
    {
        get => _japaneseWriting;
        set
        {
            _japaneseWriting = value;
            _japaneseWriting = WanaKana.ToHiragana(_japaneseWriting);
            Romaji = WanaKana.ToRomaji(_japaneseWriting);
        }
    }
}