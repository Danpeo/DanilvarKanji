namespace DanilvarKanji.Models;

public class WordMeaning
{
    public int Id { get; set; }
    public string Definition { get; set; }
    public float? Priority { get; set; }
    public Word Word { get; set; }
}