namespace DanilvarKanji.Domain.Primitives.Result;

public class ValidationRes<T> : Result<T>, IValidationResult
{
    private ValidationRes(Error[] errors) : base(default, isSuccess: false, IValidationResult.ValidationError)
    {
        Errors = errors;
    }
    
    public Error[] Errors { get; }

    public static ValidationRes<T> WithErrors(Error[] errors)
    {
        return new ValidationRes<T>(errors);
    }
}