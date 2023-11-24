using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DanilvarKanji.Application.Auth.Commands;

public class ConfirmEmailCommand : IRequest<IdentityResult>
{
    public string UserId { get; }

    public ConfirmEmailCommand(string userId)
    {
        UserId = userId;
    }
}