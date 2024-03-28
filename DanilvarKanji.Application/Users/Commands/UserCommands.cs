using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Shared.Domain.Entities;
using DanilvarKanji.Shared.Domain.Params;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DanilvarKanji.Application.Users.Commands;

public record DeleteUserCommand(string Email) : IRequest<Result<string>>;

public record UpdateUserXpCommand(int Xp, string Email) : IRequest<Result<string>>;

public record UpdateUserLearningSettingsCommand(string Email, LearningSettings LearningSettings)
    : IRequest<Result<string>>;

public record UpdateUserCommand(string Email, string NewUserName, string NewUserRole) : IRequest<Result<string>>;