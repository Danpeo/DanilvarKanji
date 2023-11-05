using System.ComponentModel.DataAnnotations;
using WanaKanaNet;

namespace DanilvarKanji.Shared.Models.Common;

public class Reading
{
    private string _japaneseWriting = string.Empty;

    [Key]
    public string Id { get; set; }

    public virtual string JapaneseWriting
    {
        get => _japaneseWriting;
        set => _japaneseWriting = WanaKana.ToKana(value);
    }

    public string? Romaji { get; set; }

    public Reading()
    {
        Id = Guid.NewGuid().ToString("N");
    }
}