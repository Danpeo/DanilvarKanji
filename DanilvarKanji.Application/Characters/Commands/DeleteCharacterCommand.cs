using DanilvarKanji.Domain.Primitives.Result;
using MediatR;

namespace DanilvarKanji.Application.Characters.Commands;

public record DeleteCharacterCommand(string Id) : IRequest<Result>;
public record DeleteCharactersCommand(IEnumerable<string> ids) : IRequest<Result>;
public record DeleteAllCharactersCommand() : IRequest<Result>;
