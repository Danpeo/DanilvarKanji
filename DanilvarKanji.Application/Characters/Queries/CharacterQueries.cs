using DanilvarKanji.Domain.Entities;
using DanilvarKanji.Domain.Enumerations;
using DanilvarKanji.Domain.Params;
using MediatR;

namespace DanilvarKanji.Application.Characters.Queries;

public record ListCharactersQuery(PaginationParams? PaginationParams) : IRequest<IEnumerable<Character>>;

public record ListChildCharactersQuery(string CharacterId) : IRequest<IEnumerable<Character>>;

public record GetCharacterQuery(string Id) : IRequest<Character?>;

public record SearchCharactersQuery
    (string SearchTerm, PaginationParams? PaginationParams) : IRequest<IEnumerable<Character>>;

public record GetKanjiMeaningsByPriorityQuery
    (string CharacterId, Culture Culture, int TakeQty = int.MaxValue) : IRequest<IEnumerable<string>>;

public record ListLearnQueueQuery
    (PaginationParams? PaginationParams, JlptLevel JlptLevel) : IRequest<IEnumerable<Character>>;