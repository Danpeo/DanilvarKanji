
using DanilvarKanji.Shared.Responses.Exercise;

namespace DanilvarKanji.Shared.Responses.Review;

public class GetBaseReviewSessionResponse
{
    public string Id { get; set; }
    public List<GetBaseExerciseInfoResponse> Exercises { get; set; } = new();

    public DateTime ReviewDataTime { get; set; } 
}