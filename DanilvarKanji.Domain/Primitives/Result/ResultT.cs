namespace DanilvarKanji.Domain.Primitives.Result;

public class Result<TValue> : Result
{
    private readonly TValue _value;

    protected internal Result(TValue value, bool isSuccess, Error error)
        : base(isSuccess, error)
    {
        _value = value;
    }

    public Result()
    {
    }

    public TValue Value =>
        IsSuccess
            ? _value
            : throw new InvalidOperationException("The value of a failure result can not be accessed.");

    public static implicit operator Result<TValue>(TValue value)
    {
        return Success(value);
    }
}