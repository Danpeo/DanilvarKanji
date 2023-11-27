using System.IdentityModel.Tokens.Jwt;
using DanilvarKanji.Application.Auth.Commands;
using DanilvarKanji.Domain.Entities;
using DanilvarKanji.Domain.Errors;
using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Infrastructure.Auth;
using DanilvarKanji.Shared.Responses.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DanilvarKanji.Application.Auth.Handlers;

// ReSharper disable once UnusedType.Global
public class LoginUserHandler : IRequestHandler<LoginUserCommand, Result<LoginResponse>>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IJwtProvider _jwtProvider;

    public LoginUserHandler(UserManager<AppUser> userManager, IJwtProvider jwtProvider)
    {
        _userManager = userManager;
        _jwtProvider = jwtProvider;
    }

    public async Task<Result<LoginResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        AppUser? user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
            return Result.Failure<LoginResponse>(User.NotFound);

        bool result = await _userManager.CheckPasswordAsync(user, request.Password);

        if (!result)
            return Result.Failure<LoginResponse>(User.WrongCredentials);

        JwtSecurityToken token = _jwtProvider.GenerateJwt(user);
        string refreshToken = _jwtProvider.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddMinutes(60);

        await _userManager.UpdateAsync(user);

        return Result.Success(new LoginResponse(_jwtProvider.GetTokenValue(token), refreshToken, token.ValidTo));
    }
}