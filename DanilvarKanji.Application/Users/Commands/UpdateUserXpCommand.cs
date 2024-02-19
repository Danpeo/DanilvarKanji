using DanilvarKanji.Domain.Primitives.Result;
using MediatR;

namespace DanilvarKanji.Application.Users.Commands;

public record UpdateUserXpCommand(int Xp, string Email) : IRequest<Result<string>>;