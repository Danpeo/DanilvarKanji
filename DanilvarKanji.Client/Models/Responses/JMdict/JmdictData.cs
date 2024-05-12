namespace DanilvarKanji.Client.Models.Responses.JMdict;

public class JmdictData
{
  public Guid Id { get; set; }

  public string? Version { get; set; }

  public List<string>? Languages { get; set; }

  public bool? CommonOnly { get; set; }

  public List<string>? DictRevisions { get; set; }

  public Dictionary<string, string>? Tags { get; set; }

  public ICollection<Word> Words { get; set; } = new List<Word>();
}