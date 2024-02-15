using Danilvar.Entity;

namespace DanilvarKanji.Shared.Domain.Entities;

public class Image : Entity
{
    public string Url { get; set; }
    public string PublicId { get; set; }
}