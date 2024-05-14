using DanilvarKanji.Domain.Primitives;

namespace DanilvarKanji.Domain.Errors;

public static class General
{
    public static Error UnProcessableRequest =>
        new("General.UnProcessableRequest", "The server could not process the request.");

    public static Error ServerError => new("General.ServerError", "The server encountered an unrecoverable error.");

    public static Error NotFound(string identifier)
    {
        return new Error("General.NotFound", $"The {identifier} with the specified identifier was not found.");
    }
}