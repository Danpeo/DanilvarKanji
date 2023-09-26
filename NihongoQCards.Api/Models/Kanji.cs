namespace NihongoQCards.Models;

public class Kanji
{
    public int Id { get; set; }
    public string Character { get; set; }
    public JlptLevel? JlptLevel { get; set; }
    public ICollection<Meaning> Meanings { get; set; }
    public ICollection<string>? Kunyomis { get; set; }
    public ICollection<string>? Onyomis { get; set; }
}