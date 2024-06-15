using DanilvarKanji.Domain.Enumerations;
using DanilvarKanji.Domain.Primitives;
using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Domain.Shared.Enumerations;
using MediatR;

namespace DanilvarKanji.Application.Characters.Commands;

public record DeleteCharacterCommand(string Id) : IRequest<Result>, ILocalizable
{
    public required Culture Culture { get; init; }
}

public record DeleteCharactersCommand(IEnumerable<string> ids) : IRequest<Result>, ILocalizable
{
    public required Culture Culture { get; init; }
}

public record DeleteAllCharactersCommand : IRequest<Result>;