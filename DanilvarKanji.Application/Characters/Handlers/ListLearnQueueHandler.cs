using AutoMapper;
using DanilvarKanji.Application.Characters.Queries;
using DanilvarKanji.Domain.Entities;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Shared.Responses.Character;
using MediatR;

namespace DanilvarKanji.Application.Characters.Handlers;

// ReSharper disable once UnusedType.Global
public class ListLearnQueueHandler : IRequestHandler<ListLearnQueueQuery, IEnumerable<GetCharacterBaseInfoResponse>>
{
    private readonly ICharacterRepository _characterRepository;
    private readonly IMapper _mapper;
    
    public ListLearnQueueHandler(ICharacterRepository characterRepository, IMapper mapper)
    {
        _characterRepository = characterRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetCharacterBaseInfoResponse>> Handle(ListLearnQueueQuery request,
        CancellationToken cancellationToken)
    {
        if (await _characterRepository.AnyInLearnQueue(request.AppUser))
        {
            var characters = await _characterRepository.ListLearnQueueAsync(request.PaginationParams, request.AppUser,
                request.JlptLevel);

            return _mapper.Map<IEnumerable<GetCharacterBaseInfoResponse>>(characters);
        }
        
        return Enumerable.Empty<GetCharacterBaseInfoResponse>();
    }
}