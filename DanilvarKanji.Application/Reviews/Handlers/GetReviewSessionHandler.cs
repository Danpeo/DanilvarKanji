using DanilvarKanji.Application.Reviews.Queries;
using DanilvarKanji.Domain.Entities;
using DanilvarKanji.Domain.RepositoryAbstractions;
using MediatR;

namespace DanilvarKanji.Application.Reviews.Handlers;

public class GetReviewSessionHandler : IRequestHandler<GetReviewSessionQuery, ReviewSession?>
{
    private readonly IReviewRepository _reviewRepository;

    public GetReviewSessionHandler(IReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }

    public async Task<ReviewSession?> Handle(GetReviewSessionQuery request, CancellationToken cancellationToken)
    {
        if (await _reviewRepository.ExistAsync(request.Id, request.AppUser))
            return await _reviewRepository.GetAsync(request.Id, request.AppUser);

        return null;
    }
}