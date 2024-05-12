using DanilvarKanji.Shared.Requests.Reviews;
using DanilvarKanji.Shared.Responses.Review;

namespace DanilvarKanji.Client.Services.Review;

public interface IReviewService
{
  Task<ReviewSessionResponseBase?> CreateReviewSessionAsync(CreateReviewSessionRequest request);

  Task<ReviewSessionResponseBase?> GetReviewSessionAsync(string? id);
}