using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Domain.Shared.Entities;
using MediatR;

namespace DanilvarKanji.Application.CharacterLearnings.Commands;

public record ToggleSkipStateCommand(string Id, AppUser AppUser) : IRequest<Result<string>>;