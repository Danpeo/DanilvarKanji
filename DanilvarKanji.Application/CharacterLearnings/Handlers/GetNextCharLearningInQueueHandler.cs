using AutoMapper;
using DanilvarKanji.Application.CharacterLearnings.Queries;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Domain.Shared.Entities;
using DanilvarKanji.Shared.Responses.CharacterLearning;
using MediatR;

namespace DanilvarKanji.Application.CharacterLearnings.Handlers;

// ReSharper disable once UnusedType.Global
public class GetNextCharLearningInQueueHandler
    : IRequestHandler<GetNextToReviewInQueueQuery, CharacterLearningResponseBase?>
{
    private readonly ICharacterLearningRepository _characterLearningRepository;
    private readonly IMapper _mapper;

    public GetNextCharLearningInQueueHandler(
        ICharacterLearningRepository characterLearningRepository,
        IMapper mapper
    )
    {
        _characterLearningRepository = characterLearningRepository;
        _mapper = mapper;
    }

    public async Task<CharacterLearningResponseBase?> Handle(
        GetNextToReviewInQueueQuery request,
        CancellationToken cancellationToken
    )
    {
        if (await _characterLearningRepository.AnyToReview(request.AppUser))
        {
            CharacterLearning? learning = await _characterLearningRepository.GetNextInReviewQueue(
                request.AppUser
            );
            return _mapper.Map<CharacterLearningResponseBase>(learning);
        }

        return null;
    }
}