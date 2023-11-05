using DanilvarKanji.Shared.Models.Common;
using WanaKanaNet;

namespace DanilvarKanji.Shared.Models;

public class Onyomi : Reading
{
    private string _onyomi = string.Empty;

    public override string JapaneseWriting
    {
        get => _onyomi;
        set => _onyomi = WanaKana.ToKatakana(value);
    }
}