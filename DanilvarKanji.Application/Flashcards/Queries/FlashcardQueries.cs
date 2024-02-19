using DanilvarKanji.Shared.Domain.Entities;
using DanilvarKanji.Shared.Domain.Entities.Flashcards;
using DanilvarKanji.Shared.Domain.Params;
using MediatR;

namespace DanilvarKanji.Application.Flashcards.Queries;

public record ListFlashcardCollectionsQuery
(
    PaginationParams? PaginationParams,
    AppUser AppUser
) : IRequest<IEnumerable<FlashcardCollection>>;