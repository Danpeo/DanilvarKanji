using DanilvarKanji.Application.CharacterLearnings.Queries;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Domain.Shared.Entities;
using MediatR;

namespace DanilvarKanji.Application.CharacterLearnings.Handlers;

public class ListCompletelyLearnedHandler
    : IRequestHandler<ListCompletelyLearnedQuery, IEnumerable<CharacterLearning>>
{
    private readonly ICharacterLearningRepository _characterLearningRepository;

    public ListCompletelyLearnedHandler(ICharacterLearningRepository characterLearningRepository)
    {
        _characterLearningRepository = characterLearningRepository;
    }

    public async Task<IEnumerable<CharacterLearning>> Handle(
        ListCompletelyLearnedQuery request,
        CancellationToken cancellationToken
    )
    {
        if (await _characterLearningRepository.AnyExistAsync())
            return await _characterLearningRepository.ListCompletelyLearnedCharactersAsync(
                request.PaginationParams,
                request.AppUser
            );

        return Enumerable.Empty<CharacterLearning>();
    }
}