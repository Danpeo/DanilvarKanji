using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Shared.Domain.Params;
using MediatR;

namespace DanilvarKanji.Application.Users.Commands;

public record UpdateUserLearningSettingsCommand(string Email, LearningSettings LearningSettings) : IRequest<Result<string>>;