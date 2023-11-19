using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Shared.Responses.Auth;
using MediatR;

namespace DanilvarKanji.Application.Auth.Commands;

public class RegisterUserCommand : IRequest<Result<TokenResponse>>
{
    public string UserName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string PasswordRepeat { get; set; }

    public RegisterUserCommand(string userName, string email, string password, string passwordRepeat)
    {
        UserName = userName;
        Email = email;
        Password = password;
        PasswordRepeat = passwordRepeat;
    }
}