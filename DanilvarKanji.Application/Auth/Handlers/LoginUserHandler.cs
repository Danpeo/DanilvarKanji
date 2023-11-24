using DanilvarKanji.Application.Auth.Commands;
using DanilvarKanji.Domain.Entities;
using DanilvarKanji.Domain.Errors;
using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Infrastructure.Auth;
using DanilvarKanji.Shared.Responses.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DanilvarKanji.Application.Auth.Handlers;

public class LoginUserHandler : IRequestHandler<LoginUserCommand, Result<TokenResponse>>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IJwtProvider _jwtProvider;

    public LoginUserHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IJwtProvider jwtProvider)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtProvider = jwtProvider;
    }

    public async Task<Result<TokenResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        AppUser? user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
            return Result.Failure<TokenResponse>(User.NotFound);
        
        bool result = await _userManager.CheckPasswordAsync(user, request.Password);
        
        if (!result)
            return Result.Failure<TokenResponse>(User.WrongCredentials);

        string token = _jwtProvider.Create(user);

        return Result.Success(new TokenResponse(token));
    }
}