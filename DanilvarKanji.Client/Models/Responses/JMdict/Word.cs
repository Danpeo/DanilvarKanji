namespace DanilvarKanji.Client.Models.Responses.JMdict;

public class Word
{
  public string Id { get; set; } = null!;

  public Guid? JmdictDataId { get; set; }

  public JmdictData? JmdictData { get; set; }

  public ICollection<Kana> Kanas { get; set; } = new List<Kana>();

  public ICollection<Kanji> Kanjis { get; set; } = new List<Kanji>();

  public ICollection<Sense> Senses { get; set; } = new List<Sense>();
}