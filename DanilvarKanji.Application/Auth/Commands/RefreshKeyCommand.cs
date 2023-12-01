using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Shared.Responses.Auth;
using MediatR;

namespace DanilvarKanji.Application.Auth.Commands;

public class RefreshKeyCommand : IRequest<Result<LoginResponse>>
{
    public required string AccessToken { get; set; }
    public required string RefreshToken { get; set; }
}