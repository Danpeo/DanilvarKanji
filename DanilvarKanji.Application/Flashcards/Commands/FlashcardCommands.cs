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

