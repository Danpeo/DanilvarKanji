using DanilvarKanji.Domain.Primitives.Result;
using MediatR;

namespace DanilvarKanji.Application.Auth.Commands;

public record RequestToChangePasswordCommand(string Email) : IRequest<Result<string>>;

public record ChangePasswordCommand(string Email, string ConfirmationCode, string NewPassword)
    : IRequest<Result<string>>;