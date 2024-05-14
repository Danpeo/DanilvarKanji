using DanilvarKanji.Domain.Shared.Entities;
using DanilvarKanji.Domain.Shared.Enumerations;
using DanilvarKanji.Domain.Shared.Params;
using DanilvarKanji.Shared.Responses.Character;
using MediatR;

namespace DanilvarKanji.Application.Characters.Queries;

public record ListCharactersQuery(PaginationParams? PaginationParams)
    : IRequest<IEnumerable<Character>>;

public record ListChildCharactersQuery(string CharacterId) : IRequest<IEnumerable<Character>>;

public record GetCharacterQuery(string Id) : IRequest<Character?>;

public record SearchCharactersQuery(string SearchTerm, PaginationParams? PaginationParams)
    : IRequest<IEnumerable<Character>>;

public record GetKanjiMeaningsByPriorityQuery(
    string CharacterId,
    Culture Culture,
    int TakeQty = int.MaxValue
) : IRequest<IEnumerable<string>>;

public record ListLearnQueueQuery(
    PaginationParams? PaginationParams,
    JlptLevel JlptLevel,
    AppUser AppUser,
    bool listOnlyDayDosage
) : IRequest<IEnumerable<CharacterResponseBase>>;

public record GetNextInLearnQueueQuery(AppUser AppUser) : IRequest<CharacterResponseBase?>;