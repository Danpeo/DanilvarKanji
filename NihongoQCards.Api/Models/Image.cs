namespace DanilvarKanji.Models;

public class Image
{
    public string Id { get; set; }
    public string Url { get; set; }
    public string PublicId { get; set; }

    public Image()
    {
        Id = Guid.NewGuid().ToString("N");
    }
}