using DanilvarKanji.Domain.Entities;
using DanilvarKanji.Shared.Responses.Exercise;

namespace DanilvarKanji.Shared.Responses.Review;

public class GetBaseReviewSessionResponse
{
    public string Id { get; set; }
    public List<GetAllFromExerciseResponse> Exercises { get; set; } = new();

    public AppUser AppUser { get; set; }

    public DateTime ReviewDataTime { get; set; } 
}