using DanilvarKanji.Domain.Primitives;

namespace DanilvarKanji.Controllers;

public class ApiErrorResponse
{
  /// <summary>
  ///     Initializes a new instance of the <see cref="ApiErrorResponse" /> class.
  /// </summary>
  /// <param name="errors">The enumerable collection of errors.</param>
  public ApiErrorResponse(IReadOnlyCollection<Error> errors)
    {
        Errors = errors;
    }

  /// <summary>
  ///     Gets the errors.
  /// </summary>
  public IReadOnlyCollection<Error> Errors { get; }
}