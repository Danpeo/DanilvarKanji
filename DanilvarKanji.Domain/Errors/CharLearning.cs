using DanilvarKanji.Domain.Primitives;

namespace DanilvarKanji.Domain.Errors;

public static class CharLearning
{
    public static Error NotFoundInReview =>
        new("CharLearning.NotFound", "Speciefied character learning was not found in review");
}