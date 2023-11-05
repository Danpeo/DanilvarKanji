using DanilvarKanji.Shared.Models.Common;
using WanaKanaNet;

namespace DanilvarKanji.Shared.Models;

public class Kunyomi : Reading
{
    private string _kunyomi = string.Empty;

    public override string JapaneseWriting
    {
        get => _kunyomi;
        set => _kunyomi = WanaKana.ToHiragana(value);
    }
}