using DanilvarKanji.Domain.Enumerations;
using DanilvarKanji.Domain.Primitives;
using DanilvarKanji.Domain.Shared.Enumerations;

namespace DanilvarKanji.Domain.Errors;

public static class CharacterErr
{
    public static Error NotFound(Culture culture)
    {
        string code = culture switch
        {
            Culture.RuRU => "Иероглиф.НеНайден",
            Culture.EnUS => "Character.NotFound",
            _ => "Character.NotFound"
        };

        string message = culture switch
        {
            Culture.RuRU => "Иероглиф с указанным идентификатором не был найден.",
            Culture.EnUS => "The character with the specified identifier was not found.",
            _ => "The character with the specified identifier was not found."
        };
        
        return new Error(code, message);
    }

    public static Error NoCharacters =>
        new("Character.NoCharacters", "No characters with the specified options");
}