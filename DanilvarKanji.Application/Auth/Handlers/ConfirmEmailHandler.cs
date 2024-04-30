using DanilvarKanji.Application.Auth.Commands;
using DanilvarKanji.Domain.Errors;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Domain.Shared.Entities;
using DanilvarKanji.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DanilvarKanji.Application.Auth.Handlers;

// ReSharper disable once UnusedType.Global
public class ConfirmEmailHandler : IRequestHandler<ConfirmEmailCommand, IdentityResult>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ConfirmEmailHandler(UserManager<AppUser> userManager, IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IdentityResult> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        AppUser? user = await _userManager.FindByEmailAsync(request.UserEmail);
        
        if (user == null)
            return IdentityResult.Failed(Identity.NotFound);

        if (user.EmailConfirmed)
            return IdentityResult.Failed(Identity.EmailAlreadyConfirmed);
        
        string? expectedCode = await _userRepository.GetRegistrationConfirmationCodeAsync(request.UserEmail);

        if (expectedCode != null)
        {
            bool confirmed = expectedCode == request.ConfirmationCode;
            if (confirmed)
            {
                user.EmailConfirmed = true;
                await _userRepository.DeleteEmailCodeAsync(request.UserEmail);
                await _unitOfWork.CompleteAsync();
            }
        }
        else
        {
            return IdentityResult.Failed(Identity.ConfirmationCodeIsNotValid);
        }

        IdentityResult result = await _userManager.UpdateAsync(user);

        return result;
    }
}