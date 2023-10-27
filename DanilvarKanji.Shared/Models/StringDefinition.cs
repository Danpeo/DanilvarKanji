using System.Globalization;
using DanilvarKanji.Shared.Models.Enums;

namespace DanilvarKanji.Shared.Models;

public class StringDefinition
{
    public string Id { get; set; }
    public string Value { get; set; }
    public Culture Culture { get; set; }

    public StringDefinition()
    {
        Id = Guid.NewGuid().ToString("N");
    }
}