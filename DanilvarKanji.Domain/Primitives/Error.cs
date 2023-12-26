using Danilvar.ValueObject;

namespace DanilvarKanji.Domain.Primitives;

/// <summary>
/// Represents a concrete domain error.
/// </summary>
public sealed class Error : ValueObject
{
    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }


    public string Code { get; }


    public string Message { get; }

    public static implicit operator string(Error error) => error?.Code ?? string.Empty;

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Code;
        yield return Message;
    }

    internal static Error None => new Error(string.Empty, string.Empty);
}