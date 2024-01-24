using DanilvarKanji.Shared.Requests.Reviews;
using DanilvarKanji.Shared.Responses.Review;

namespace DanilvarKanji.Client.Services.Review;

public interface IReviewService
{
    Task<GetBaseReviewSessionResponse?> CreateReviewSessionAsync(CreateReviewSessionRequest request);

    Task<GetBaseReviewSessionResponse?> GetReviewSessionAsync(string? id);
}