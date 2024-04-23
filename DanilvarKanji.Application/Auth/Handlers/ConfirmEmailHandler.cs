using DanilvarKanji.Application.Auth.Commands;
using DanilvarKanji.Domain.Errors;
using DanilvarKanji.Domain.Shared.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DanilvarKanji.Application.Auth.Handlers;

// ReSharper disable once UnusedType.Global
public class ConfirmEmailHandler : IRequestHandler<ConfirmEmailCommand, IdentityResult>
{
    private readonly UserManager<AppUser> _userManager;

    public ConfirmEmailHandler(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IdentityResult> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        AppUser? user = await _userManager.FindByIdAsync(request.UserId);
        
        if (user == null)
            return IdentityResult.Failed(Identity.NotFound);

        if (user.EmailConfirmed)
            return IdentityResult.Failed(Identity.EmailAlreadyConfirmed);
        
        user.EmailConfirmed = true;

        IdentityResult result = await _userManager.UpdateAsync(user);

        return result;
    }
}