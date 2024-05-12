using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Shared.Responses.Auth;
using MediatR;

namespace DanilvarKanji.Application.Auth.Commands;

public record RefreshKeyCommand(string AccessToken, string RefreshToken)
  : IRequest<Result<LoginResponse>>;