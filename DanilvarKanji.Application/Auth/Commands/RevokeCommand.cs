using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DanilvarKanji.Application.Auth.Commands;

public class RevokeCommand : IRequest<IdentityResult>
{
    /*public string Email { get; set; }

    public RevokeCommand(string email)
    {
        Email = email;
    }*/
}