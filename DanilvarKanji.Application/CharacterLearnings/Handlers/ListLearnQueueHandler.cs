using DanilvarKanji.Application.CharacterLearnings.Queries;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Shared.Domain.Entities;
using MediatR;

namespace DanilvarKanji.Application.CharacterLearnings.Handlers;

// ReSharper disable once UnusedType.Global
public class ListLearnQueueHandler : IRequestHandler<ListLearnQueueQuery, IEnumerable<CharacterLearning>>
{
    private readonly ICharacterLearningRepository _characterLearningRepository;

    public ListLearnQueueHandler(ICharacterLearningRepository characterLearningRepository)
    {
        _characterLearningRepository = characterLearningRepository;
    }

    public async Task<IEnumerable<CharacterLearning>> Handle(ListLearnQueueQuery request,
        CancellationToken cancellationToken)
    {
        if (await _characterLearningRepository.AnyExist())
            return await _characterLearningRepository.ListLearnQueueAsync(request.PaginationParams, request.AppUser,
                request.JlptLevel);

        return Enumerable.Empty<CharacterLearning>();
    }
}