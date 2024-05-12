using FluentValidation;

namespace DanilvarKanji.Shared.Validation;

public class ValidatorBase<T> : AbstractValidator<T>
{
  protected static bool HaveCollectionWithAtLeastOneElement<TCollection>(
    ICollection<TCollection>? collection
  )
  {
    return collection is { Count: > 0 };
  }
}