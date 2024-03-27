using DanilvarKanji.Domain.Primitives.Result;
using MediatR;

namespace DanilvarKanji.Application.Users.Commands;

public record DeleteUserCommand(string Email) : IRequest<Result<string>>;