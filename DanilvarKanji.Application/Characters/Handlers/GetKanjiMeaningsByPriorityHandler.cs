using DanilvarKanji.Application.Characters.Queries;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Domain.Shared.Entities;
using MediatR;

namespace DanilvarKanji.Application.Characters.Handlers;

public class GetKanjiMeaningsByPriorityHandler
    : IRequestHandler<GetKanjiMeaningsByPriorityQuery, IEnumerable<string>>
{
    private readonly ICharacterRepository _characterRepository;

    public GetKanjiMeaningsByPriorityHandler(
        ICharacterRepository characterRepository
    )
    {
        _characterRepository = characterRepository;
    }

    public async Task<IEnumerable<string>> Handle(
        GetKanjiMeaningsByPriorityQuery request,
        CancellationToken cancellationToken
    )
    {
        if (!await _characterRepository.ExistAsync(request.CharacterId))
            return Enumerable.Empty<string>();

        Character? character = await _characterRepository.GetAsync(request.CharacterId);

        var kanjiMeanings = await _characterRepository.GetKanjiMeaningsByPriority(
            character!.Id,
            request.TakeQty,
            request.Culture
        );

        return kanjiMeanings;
    }
}