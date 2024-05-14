using DanilvarKanji.Application.Auth.Commands;
using DanilvarKanji.Domain.Errors;
using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Domain.Shared.Entities;
using DanilvarKanji.Infrastructure.Data;
using DanilvarKanji.Infrastructure.Emails;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DanilvarKanji.Application.Auth.Handlers;

public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand, Result<string>>
{
    private readonly IEmailService _emailService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<AppUser> _userManager;
    private readonly IUserRepository _userRepository;

    public ChangePasswordHandler(
        UserManager<AppUser> userManager,
        IUnitOfWork unitOfWork,
        IUserRepository userRepository,
        IEmailService emailService
    )
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _emailService = emailService;
    }

    public async Task<Result<string>> Handle(
        ChangePasswordCommand request,
        CancellationToken cancellationToken
    )
    {
        /*AppUser? user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
            return Result.Failure<string>(General.UnProcessableRequest);

        await _emailService.CheckConfirmationCodeAsync(request.Email, request.ConfirmationCode, onConfirmedAsync,)

        async Task onConfirmedAsync()
        {
            await _userManager.res
            await _unitOfWork.CompleteAsync();
        }*/

        return Result.Failure<string>(General.UnProcessableRequest);
    }
}