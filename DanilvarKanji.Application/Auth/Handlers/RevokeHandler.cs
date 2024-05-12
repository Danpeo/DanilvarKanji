using DanilvarKanji.Application.Auth.Commands;
using DanilvarKanji.Domain.Shared.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DanilvarKanji.Application.Auth.Handlers;

// ReSharper disable once UnusedType.Global
public class RevokeHandler : IRequestHandler<RevokeCommand, IdentityResult>
{
  private readonly UserManager<AppUser> _userManager;

  public RevokeHandler(UserManager<AppUser> userManager)
  {
    _userManager = userManager;
  }

  public async Task<IdentityResult> Handle(
    RevokeCommand request,
    CancellationToken cancellationToken
  )
  {
    /*var username = HttpContext.User.Identity?.Name;


    var user = await _userManager.FindByEmailAsync(request.Email);

    if (user is null)
        return IdentityResult.Failed(Identity.NotFound);

    user.RefreshToken = null;

    await _userManager.UpdateAsync(user);*/

    return IdentityResult.Success;
  }
}