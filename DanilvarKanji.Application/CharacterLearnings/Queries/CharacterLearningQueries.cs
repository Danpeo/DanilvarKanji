using DanilvarKanji.Domain.Shared.Entities;
using DanilvarKanji.Domain.Shared.Enumerations;
using DanilvarKanji.Domain.Shared.Params;
using DanilvarKanji.Shared.Responses.CharacterLearning;
using MediatR;

namespace DanilvarKanji.Application.CharacterLearnings.Queries;

public record ListLearnQueueQuery(
  PaginationParams? PaginationParams,
  JlptLevel JlptLevel,
  AppUser AppUser,
  bool ListOnlyDayDosage
) : IRequest<IEnumerable<CharacterLearning>>;

public record GetCharacterLearningQuery(string Id, AppUser AppUser) : IRequest<CharacterLearning?>;

public record ListCurrentReviewQuery(PaginationParams? PaginationParams, AppUser AppUser)
  : IRequest<IEnumerable<CharacterLearningResponseBase>>;

public record ListFutureReviewQuery(PaginationParams? PaginationParams, AppUser AppUser)
  : IRequest<IEnumerable<CharacterLearningResponseBase>>;

public record ListSkippedQuery(PaginationParams? PaginationParams, AppUser AppUser)
  : IRequest<IEnumerable<CharacterLearning>>;

public record ListCompletelyLearnedQuery(PaginationParams? PaginationParams, AppUser AppUser)
  : IRequest<IEnumerable<CharacterLearning>>;

public record GetNextToReviewInQueueQuery(AppUser AppUser)
  : IRequest<CharacterLearningResponseBase?>;

public record GetRandomMeaningsInReviewQuery(
  string CharacterId,
  AppUser AppUser,
  Culture Culture,
  int Qty
) : IRequest<RandomItemsInReview?>;

public record GetRandomKunReadingsInReviewQuery(string CharacterId, AppUser AppUser, int Qty)
  : IRequest<(List<string> random, string correct)>;

public record GetRandomOnReadingsInReviewQuery(string CharacterId, AppUser AppUser, int Qty)
  : IRequest<(List<string> random, string correct)>;