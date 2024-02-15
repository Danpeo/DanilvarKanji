using Danilvar.Entity;
using DanilvarKanji.Shared.Domain.Enumerations;

namespace DanilvarKanji.Shared.Domain.Entities;

public class StringDefinition : Entity
{
    public string Value { get; set; } = string.Empty;
    public Culture Culture { get; set; }

    public StringDefinition()
    {
        
    }
    
    public StringDefinition(string value, Culture culture)
    {
        Value = value;
        Culture = culture;
    }
}