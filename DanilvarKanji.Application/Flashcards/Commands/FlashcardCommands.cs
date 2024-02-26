using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Shared.Domain.Entities;
using DanilvarKanji.Shared.Domain.Entities.Flashcards;
using MediatR;

namespace DanilvarKanji.Application.Flashcards.Commands;

public record CreateFlashcardCollectionCommand(
    string Name,
    List<Flashcard> Flashcards,
    AppUser AppUser
) : IRequest<Result<string>>;

public record UpdateFlashcardCollectionCommand(
    string CollectionId,
    AppUser AppUser,
    string Name,
    List<Flashcard> Flashcards) : IRequest<Result<string>>;