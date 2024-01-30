using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Shared.Domain.Entities;
using MediatR;

namespace DanilvarKanji.Application.CharacterLearnings.Commands;

public record ToggleSkipStateCommand(string Id, AppUser AppUser) : IRequest<Result<string>>;