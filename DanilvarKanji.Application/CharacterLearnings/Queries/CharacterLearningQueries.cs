using DanilvarKanji.Domain.Entities;
using DanilvarKanji.Domain.Enumerations;
using DanilvarKanji.Domain.Params;
using DanilvarKanji.Shared.Responses.CharacterLearning;
using MediatR;

namespace DanilvarKanji.Application.CharacterLearnings.Queries;

public record ListLearnQueueQuery
(PaginationParams? PaginationParams, JlptLevel JlptLevel,
    AppUser AppUser) : IRequest<IEnumerable<CharacterLearning>>;

public record GetCharacterLearningQuery(string Id, AppUser AppUser) : IRequest<CharacterLearning?>;

public record ListCharacterReviewQuery
    (PaginationParams? PaginationParams, AppUser AppUser) : IRequest<IEnumerable<GetCharacterLearningBaseInfoResponse>>;

public record GetNextToReviewInQueueQuery(AppUser AppUser) : IRequest<GetCharacterLearningBaseInfoResponse?>;