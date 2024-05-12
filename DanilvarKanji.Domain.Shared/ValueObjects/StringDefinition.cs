using Danilvar.ValueObject;
using DanilvarKanji.Domain.Shared.Enumerations;
using Microsoft.EntityFrameworkCore;

namespace DanilvarKanji.Domain.Shared.ValueObjects;

[Owned]
public class StringDefinition : ValueObject
{
  public StringDefinition()
  {
  }

  public StringDefinition(string value, Culture culture)
  {
    Value = value;
    Culture = culture;
  }

  public string Value { get; set; } = string.Empty;
  public Culture Culture { get; set; }

  public override IEnumerable<object> GetAtomicValues()
  {
    yield return Value;
    yield return Culture;
  }
}