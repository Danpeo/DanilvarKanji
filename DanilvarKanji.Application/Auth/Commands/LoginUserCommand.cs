using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Shared.Responses.Auth;
using MediatR;

namespace DanilvarKanji.Application.Auth.Commands;

public record LoginUserCommand(string Email, string Password) : IRequest<Result<LoginResponse>>;