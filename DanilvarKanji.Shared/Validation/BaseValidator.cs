using FluentValidation;

namespace DanilvarKanji.Shared.Validation;

public class BaseValidator<T> : AbstractValidator<T>
{
    protected static bool HaveAtLeastOneCollection<TCollection>(ICollection<TCollection>? collection) => 
        collection is { Count: > 0 };
}