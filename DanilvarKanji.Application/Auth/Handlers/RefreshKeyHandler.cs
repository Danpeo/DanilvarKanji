using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using DanilvarKanji.Application.Auth.Commands;
using DanilvarKanji.Domain.Errors;
using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Domain.Shared.Entities;
using DanilvarKanji.Infrastructure.Auth;
using DanilvarKanji.Shared.Responses.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DanilvarKanji.Application.Auth.Handlers;

// ReSharper disable once UnusedType.Global
public class RefreshKeyHandler : IRequestHandler<RefreshKeyCommand, Result<LoginResponse>>
{
    private readonly IJwtProvider _jwtProvider;
    private readonly UserManager<AppUser> _userManager;

    public RefreshKeyHandler(IJwtProvider jwtProvider, UserManager<AppUser> userManager)
    {
        _jwtProvider = jwtProvider;
        _userManager = userManager;
    }

    public async Task<Result<LoginResponse>> Handle(
        RefreshKeyCommand request,
        CancellationToken cancellationToken
    )
    {
        ClaimsPrincipal? principal = _jwtProvider.GetPrincipalFromExpiredToken(request.AccessToken);

        if (principal?.Identity?.Name is null)
            return Result.Failure<LoginResponse>(User.NotFound);

        AppUser? user = await _userManager.FindByNameAsync(principal.Identity.Name);

        if (
            user is null
            || user.RefreshToken != request.RefreshToken
            || user.RefreshTokenExpiry < DateTime.UtcNow
        )
            return Result.Failure<LoginResponse>(User.NotFound);

        JwtSecurityToken token = _jwtProvider.GenerateJwt(user);

        return Result.Success(
            new LoginResponse(_jwtProvider.GetTokenValue(token), request.RefreshToken, token.ValidTo)
        );
    }
}