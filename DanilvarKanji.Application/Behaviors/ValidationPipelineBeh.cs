using DanilvarKanji.Domain.Primitives;
using DanilvarKanji.Domain.Primitives.Result;
using FluentValidation;
using MediatR;

namespace DanilvarKanji.Application.Behaviors;

public class ValidationPipelineBeh<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
  where TRequest : IRequest<TResponse>
  where TResponse : Result
{
  private readonly IEnumerable<IValidator<TRequest>> _validators;

  public ValidationPipelineBeh(IEnumerable<IValidator<TRequest>> validators)
  {
    _validators = validators;
  }

  public async Task<TResponse> Handle(
    TRequest request,
    RequestHandlerDelegate<TResponse> next,
    CancellationToken cancellationToken
  )
  {
    if (!_validators.Any())
      return await next();

    var erros = _validators
      .Select(v => v.Validate(request))
      .SelectMany(vr => vr.Errors)
      .Where(validationFailure => validationFailure != null)
      .Select(failure => new Error(failure.PropertyName, failure.ErrorMessage))
      .Distinct()
      .ToArray();

    if (erros.Any())
      return CreateValidtionResult<TResponse>(erros);

    return await next();
  }

  private static TResult CreateValidtionResult<TResult>(Error[] errors)
    where TResult : Result
  {
    if (typeof(TResult) == typeof(Result))
      return (ValidationRes.WithErrors(errors) as TResult)!;

    var result = typeof(ValidationRes<>)
      .GetGenericTypeDefinition()
      .MakeGenericType(typeof(TResult).GenericTypeArguments[0])
      .GetMethod(nameof(ValidationRes.WithErrors))!
      .Invoke(null, new object?[] { errors });

    return (TResult)result!;
  }
}