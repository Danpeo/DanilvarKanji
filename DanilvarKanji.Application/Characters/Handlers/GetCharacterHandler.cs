using DanilvarKanji.Application.Characters.Queries;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Domain.Shared.Entities;
using MediatR;

namespace DanilvarKanji.Application.Characters.Handlers;

// ReSharper disable once UnusedType.Global
public class GetCharacterHandler : IRequestHandler<GetCharacterQuery, Character?>
{
    private readonly ICharacterRepository _characterRepository;

    public GetCharacterHandler(ICharacterRepository characterRepository)
    {
        _characterRepository = characterRepository;
    }

    public async Task<Character?> Handle(
        GetCharacterQuery request,
        CancellationToken cancellationToken
    )
    {
        if (await _characterRepository.ExistAsync(request.Id))
            return await _characterRepository.GetAsync(request.Id);

        return null;
    }
}