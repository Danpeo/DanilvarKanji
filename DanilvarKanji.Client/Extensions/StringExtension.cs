using System.Text;
using WanaKanaNet;

namespace DanilvarKanji.Client.Extensions;

public static class StringExtension
{
    public static string IgnoreNonJapanese(this string str)
    {
        var stringBuilder = new StringBuilder();

        foreach (char c in str.Where(WanaKana.IsJapanese))
        {
            stringBuilder.Append(c);
        }

        return str;
    }
}