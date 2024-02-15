
using DanilvarKanji.Shared.Responses.Exercise;

namespace DanilvarKanji.Shared.Responses.Review;

public class ReviewSessionResponseBase
{
    public string Id { get; set; }
    public List<ExerciseResponseBase> Exercises { get; set; } = new();

    public DateTime ReviewDataTime { get; set; } 
}