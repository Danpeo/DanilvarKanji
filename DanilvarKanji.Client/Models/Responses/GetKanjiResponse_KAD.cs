namespace DanilvarKanji.Client.Models.Responses;

public class GetKanjiResponse_KAD
{
  public int Grade { get; set; }
  public int Jlpt { get; set; }
  public string Kanji { get; set; } = "";
  public List<string> Kun_Readings { get; set; } = new();
  public List<string> Meanings { get; set; } = new();
  public List<string> On_Readings { get; set; } = new();
  public int Stroke_Count { get; set; }
}