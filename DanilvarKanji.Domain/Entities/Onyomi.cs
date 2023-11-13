using WanaKanaNet;

namespace DanilvarKanji.Domain.Entities;

public class Onyomi : Reading
{
    private string _onyomi = string.Empty;

    public override string JapaneseWriting
    {
        get => _onyomi;
        set => _onyomi = WanaKana.ToKatakana(value);
    }
}