using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Domain.Shared.Params;
using MediatR;

namespace DanilvarKanji.Application.Users.Commands;

public record DeleteUserCommand(string Email) : IRequest<Result<string>>;

public record UpdateUserXpCommand(int Xp, string Email) : IRequest<Result<string>>;

public record UpdateUserLearningSettingsCommand(string Email, LearningSettings LearningSettings)
    : IRequest<Result<string>>;

public record UpdateUserCommand(string Email, string NewUserName, string NewUserRole) : IRequest<Result<string>>;