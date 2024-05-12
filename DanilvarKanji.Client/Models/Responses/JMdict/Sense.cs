namespace DanilvarKanji.Client.Models.Responses.JMdict;

public class Sense
{
  public Guid Id { get; set; }

  public List<string>? PartOfSpeech { get; set; }

  public List<string>? AppliesToKanji { get; set; }

  public List<string>? AppliesToKana { get; set; }

  public List<string>? Related { get; set; }

  public List<string>? Antonym { get; set; }

  public List<string>? Field { get; set; }

  public List<string>? Dialect { get; set; }

  public List<string>? Misc { get; set; }

  public List<string>? Info { get; set; }

  public List<string>? LanguageSource { get; set; }

  public string? WordId { get; set; }

  public List<Gloss> Glosses { get; set; } = new();

  //public virtual Word? Word { get; set; }
}