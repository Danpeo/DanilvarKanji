using System.IdentityModel.Tokens.Jwt;
using DanilvarKanji.Application.Auth.Commands;
using DanilvarKanji.Domain.Errors;
using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Domain.Settings;
using DanilvarKanji.Domain.Shared.Entities;
using DanilvarKanji.Infrastructure.Auth;
using DanilvarKanji.Shared.Responses.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace DanilvarKanji.Application.Auth.Handlers;

// ReSharper disable once UnusedType.Global
public class LoginUserHandler : IRequestHandler<LoginUserCommand, Result<LoginResponse>>
{
    private readonly IJwtProvider _jwtProvider;
    private readonly JwtSettings _jwtSettings;
    private readonly UserManager<AppUser> _userManager;

    public LoginUserHandler(
        UserManager<AppUser> userManager,
        IJwtProvider jwtProvider,
        IOptions<JwtSettings> jwtSettingsOptions
    )
    {
        _userManager = userManager;
        _jwtProvider = jwtProvider;
        _jwtSettings = jwtSettingsOptions.Value;
    }

    public async Task<Result<LoginResponse>> Handle(
        LoginUserCommand request,
        CancellationToken cancellationToken
    )
    {
        AppUser? user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
            return Result.Failure<LoginResponse>(User.NotFound);

        if (!user.EmailConfirmed)
            return Result.Failure<LoginResponse>(User.EmailNotComfirmed);

        var result = await _userManager.CheckPasswordAsync(user, request.Password);

        if (!result)
            return Result.Failure<LoginResponse>(User.WrongCredentials);

        JwtSecurityToken token = _jwtProvider.GenerateJwt(user);
        var refreshToken = _jwtProvider.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpiryDays);

        await _userManager.UpdateAsync(user);

        return Result.Success(
            new LoginResponse(_jwtProvider.GetTokenValue(token), refreshToken, token.ValidTo)
        );
    }
}