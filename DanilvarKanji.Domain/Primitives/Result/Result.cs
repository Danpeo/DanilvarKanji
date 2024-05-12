namespace DanilvarKanji.Domain.Primitives.Result;

public class Result
{
  private readonly List<Error> _errors = new();

  public Result()
  {
  }

  protected Result(bool isSuccess, Error error)
  {
    if ((isSuccess && !error.Equals(Error.None)) || (!isSuccess && error.Equals(Error.None)))
      throw new InvalidOperationException();

    IsSuccess = isSuccess;
    Error = error;
  }

  public bool IsSuccess { get; set; }

  public bool IsFailure => !IsSuccess;

  public Error Error { get; }

  public static Result Success()
  {
    return new Result(true, Error.None);
  }

  public static Result<TValue> Success<TValue>(TValue value)
  {
    return new Result<TValue>(value, true, Error.None);
  }

  public static Result<TValue> Create<TValue>(TValue? value, Error error)
    where TValue : class
  {
    return value is null ? Failure<TValue>(error) : Success(value);
  }

  public static Result Failure(Error error)
  {
    return new Result(false, error);
  }

  public static Result<TValue> Failure<TValue>(Error error)
  {
    return new Result<TValue>(default!, false, error);
  }

  public static Result Failed(params Error[]? errors)
  {
    var result = new Result { IsSuccess = false };

    if (errors != null) result._errors.AddRange(errors);
    return result;
  }

  public static Result<TValue> Failed<TValue>(params Error[]? errors)
  {
    var result = new Result<TValue> { IsSuccess = false };

    if (errors != null) result._errors.AddRange(errors);
    return result;
  }

  public static Result FirstFailureOrSuccess(params Result[] results)
  {
    foreach (Result result in results)
      if (result.IsFailure)
        return result;

    return Success();
  }
}