using DanilvarKanji.Application.Characters.Queries;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Domain.Shared.Entities;
using MediatR;

namespace DanilvarKanji.Application.Characters.Handlers;

public class SearchCharactersHandler
    : IRequestHandler<SearchCharactersQuery, IEnumerable<Character>>
{
    private readonly ICharacterRepository _characterRepository;

    public SearchCharactersHandler(ICharacterRepository characterRepository)
    {
        _characterRepository = characterRepository;
    }

    public async Task<IEnumerable<Character>> Handle(
        SearchCharactersQuery request,
        CancellationToken cancellationToken
    )
    {
        if (!await _characterRepository.AnyExistAsync())
            return Enumerable.Empty<Character>();

        if (string.IsNullOrEmpty(request.SearchTerm) || request.SearchTerm.ToLower() == "any")
            return _characterRepository.List(request.PaginationParams);

        return await _characterRepository.SearchAsync(request.SearchTerm);
    }
}