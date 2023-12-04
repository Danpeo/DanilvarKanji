using DanilvarKanji.Domain.Entities;
using DanilvarKanji.Domain.Enumerations;
using DanilvarKanji.Domain.Params;
using MediatR;

namespace DanilvarKanji.Application.CharacterLearnings.Queries;

public record ListLearnQueueQuery
    (PaginationParams? PaginationParams, JlptLevel JlptLevel, AppUser AppUser) : IRequest<IEnumerable<CharacterLearning>>;