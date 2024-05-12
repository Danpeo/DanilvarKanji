/*using DanilvarKanji.Application.Characters.Queries;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Shared.Domain.Entities;
using MediatR;

namespace DanilvarKanji.Application.Characters.Handlers;

public class ListChildCharactersHandler : IRequestHandler<ListChildCharactersQuery, IEnumerable<Character>>
{
    private readonly ICharacterRepository _characterRepository;

    public ListChildCharactersHandler(ICharacterRepository characterRepository)
    {
        _characterRepository = characterRepository;
    }

    public async Task<IEnumerable<Character>> Handle(ListChildCharactersQuery request, CancellationToken cancellationToken)
    {
        if (!await _characterRepository.ExistAsync(request.CharacterId))
            return Enumerable.Empty<Character>();

        return await _characterRepository.ListChildCharacters(request.CharacterId);
    }
}*/

