using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Shared.Responses.Auth;
using MediatR;

namespace DanilvarKanji.Application.Auth.Commands;

public class LoginUserCommand : IRequest<Result<LoginResponse>>
{
    public string Email { get; set; }

    public string Password { get; set; }

    public LoginUserCommand(string email, string password)
    {
        Email = email;
        Password = password;
    }
}