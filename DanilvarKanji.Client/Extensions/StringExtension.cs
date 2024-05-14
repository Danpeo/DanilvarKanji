using System.Text;
using WanaKanaNet;

namespace DanilvarKanji.Client.Extensions;

public static class StringExtension
{
    public static string IgnoreNonJapanese(this string str)
    {
        var stringBuilder = new StringBuilder();

        foreach (var c in str)
            if (WanaKana.IsJapanese(c))
                stringBuilder.Append(c);

        return str;
    }
}