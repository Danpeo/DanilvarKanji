using DanilvarKanji.Domain.Shared.Entities;
using DanilvarKanji.Domain.Shared.Entities.Flashcards;
using DanilvarKanji.Domain.Shared.Params;
using MediatR;

namespace DanilvarKanji.Application.Flashcards.Queries;

public record ListFlashcardCollectionsQuery(PaginationParams? PaginationParams, AppUser AppUser)
    : IRequest<IEnumerable<FlashcardCollection>>;

public record GetFlashcardCollectionQuery(string Id, AppUser AppUser)
    : IRequest<FlashcardCollection?>;