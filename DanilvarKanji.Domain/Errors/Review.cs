using DanilvarKanji.Domain.Primitives;

namespace DanilvarKanji.Domain.Errors;

public class Review
{
  public static Error NotFound =>
    new("Review.NotFound", "The Review with the specified identifier was not found.");
}