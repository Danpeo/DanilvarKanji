using DanilvarKanji.Application.Auth.Commands;
using DanilvarKanji.Domain.Errors;
using DanilvarKanji.Domain.Shared.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DanilvarKanji.Application.Auth.Handlers;

public class ConfirmEmailForcedHandler : IRequestHandler<ConfirmEmailForcedCommand, IdentityResult>
{
  private readonly UserManager<AppUser> _userManager;

  public ConfirmEmailForcedHandler(UserManager<AppUser> userManager)
  {
    _userManager = userManager;
  }

  public async Task<IdentityResult> Handle(
    ConfirmEmailForcedCommand request,
    CancellationToken cancellationToken
  )
  {
    AppUser? user = await _userManager.FindByEmailAsync(request.UserEmail);

    if (user == null)
      return IdentityResult.Failed(Identity.NotFound);

    if (user.EmailConfirmed)
      return IdentityResult.Failed(Identity.EmailAlreadyConfirmed);

    user.EmailConfirmed = true;
    IdentityResult result = await _userManager.UpdateAsync(user);

    return result;
  }
}