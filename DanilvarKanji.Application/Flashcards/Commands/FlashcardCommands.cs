using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Domain.Shared.Entities;
using DanilvarKanji.Domain.Shared.Entities.Flashcards;
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
  List<Flashcard> Flashcards
) : IRequest<Result<string>>;

public record DeleteCollectionCommand(string CollectionId, AppUser AppUser)
  : IRequest<Result<string>>;