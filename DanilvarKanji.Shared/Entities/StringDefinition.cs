using DanilvarKanji.Shared.Entities.Enums;

namespace DanilvarKanji.Shared.Entities;

public class StringDefinition
{
    public string Id { get; set; }
    public string Value { get; set; } = string.Empty;
    public Culture Culture { get; set; } = Culture.EnUS;

    public StringDefinition()
    {
        Id = Guid.NewGuid().ToString("N");
    }
}