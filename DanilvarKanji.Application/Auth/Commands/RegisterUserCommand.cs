using DanilvarKanji.Domain.Enumerations;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DanilvarKanji.Application.Auth.Commands;

public class RegisterUserCommand : IRequest<IdentityResult>
{
    public string UserName { get; set; }

    public JlptLevel JlptLevel { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string PasswordRepeat { get; set; }

    public RegisterUserCommand(string userName, string email, string password, string passwordRepeat, JlptLevel jlptLevel)
    {
        UserName = userName;
        Email = email;
        Password = password;
        PasswordRepeat = passwordRepeat;
        JlptLevel = jlptLevel;
    }
}