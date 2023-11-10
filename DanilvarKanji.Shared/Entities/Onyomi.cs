using DanilvarKanji.Shared.Entities.Common;
using WanaKanaNet;

namespace DanilvarKanji.Shared.Entities;

public class Onyomi : Reading
{
    private string _onyomi = string.Empty;

    public override string JapaneseWriting
    {
        get => _onyomi;
        set => _onyomi = WanaKana.ToKatakana(value);
    }
}