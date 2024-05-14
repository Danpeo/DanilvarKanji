using Danilvar.ValueObject;

namespace DanilvarKanji.Domain.Primitives;

public sealed class Error : ValueObject
{
    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public string Code { get; }

    public string Message { get; }

    internal static Error None => new(string.Empty, string.Empty);

    public static implicit operator string(Error error)
    {
        return error.Code;
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Code;
        yield return Message;
    }
}