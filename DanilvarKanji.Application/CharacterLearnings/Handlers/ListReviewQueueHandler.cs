using AutoMapper;
using DanilvarKanji.Application.CharacterLearnings.Queries;
using DanilvarKanji.Domain.Entities;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Shared.Responses.CharacterLearning;
using MediatR;

namespace DanilvarKanji.Application.CharacterLearnings.Handlers;

// ReSharper disable once UnusedType.Global
public class ListReviewQueueHandler : IRequestHandler<ListCharacterReviewQuery, IEnumerable<GetCharacterLearningBaseInfoResponse>>
{
    private readonly ICharacterLearningRepository _characterLearningRepository;
    private readonly IMapper _mapper;

    public ListReviewQueueHandler(ICharacterLearningRepository characterLearningRepository, IMapper mapper)
    {
        _characterLearningRepository = characterLearningRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetCharacterLearningBaseInfoResponse>> Handle(ListCharacterReviewQuery request,
        CancellationToken cancellationToken)
    {
        if (await _characterLearningRepository.AnyToReview(request.AppUser))
        {
            var charLearnings =
                await _characterLearningRepository.ListReviewQueueAsync(request.PaginationParams, request.AppUser);

            return _mapper.Map<IEnumerable<GetCharacterLearningBaseInfoResponse>>(charLearnings);
        }

        return Enumerable.Empty<GetCharacterLearningBaseInfoResponse>();
    }
}