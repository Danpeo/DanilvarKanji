namespace DanilvarKanji.Domain.Primitives.Result;

public class ValidationRes : Result, IValidationResult
{
  private ValidationRes(Error[] errors)
    : base(false, IValidationResult.ValidationError)
  {
    Errors = errors;
  }

  public Error[] Errors { get; }

  public static ValidationRes WithErrors(Error[] errors)
  {
    return new ValidationRes(errors);
  }
}