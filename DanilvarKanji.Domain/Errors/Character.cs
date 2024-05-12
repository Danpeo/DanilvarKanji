using DanilvarKanji.Domain.Primitives;

namespace DanilvarKanji.Domain.Errors;

public static class CharacterErr
{
  public static Error NotFound =>
    new("Character.NotFound", "The character with the specified identifier was not found.");

  public static Error NoCharacters =>
    new("Character.NoCharacters", "No characters with the specified options");
}