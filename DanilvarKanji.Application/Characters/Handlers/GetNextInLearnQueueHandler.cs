using AutoMapper;
using DanilvarKanji.Application.Characters.Queries;
using DanilvarKanji.Domain.Entities;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Shared.Responses.Character;
using MediatR;

namespace DanilvarKanji.Application.Characters.Handlers;

// ReSharper disable once UnusedType.Global
public class GetNextInLearnQueueHandler : IRequestHandler<GetNextInLearnQueueQuery, GetCharacterBaseInfoResponse?>
{
    private readonly ICharacterRepository _characterRepository;
    private readonly IMapper _mapper;
    
    public GetNextInLearnQueueHandler(ICharacterRepository characterRepository, IMapper mapper)
    {
        _characterRepository = characterRepository;
        _mapper = mapper;
    }

    public async Task<GetCharacterBaseInfoResponse?> Handle(GetNextInLearnQueueQuery request, CancellationToken cancellationToken)
    {
        if (await _characterRepository.AnyInLearnQueue(request.AppUser))
        {
            Character? character = await _characterRepository.GetNextInLearnQueueAsync(request.AppUser);
            return _mapper.Map<GetCharacterBaseInfoResponse>(character);
        }

        return null;
    }
}