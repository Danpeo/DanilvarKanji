using DanilvarKanji.Domain.Entities;
using DanilvarKanji.Domain.Params;
using MediatR;

namespace DanilvarKanji.Application.Characters.Queries;

public record ListCharactersQuery(PaginationParams? PaginationParams) : IRequest<IEnumerable<Character>>;