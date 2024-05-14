using DanilvarKanji.Application.Auth.Commands;
using DanilvarKanji.Domain.Entities;
using DanilvarKanji.Domain.Errors;
using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Domain.Shared.Entities;
using DanilvarKanji.Infrastructure.Data;
using DanilvarKanji.Infrastructure.Emails;
using DVar.RandCreds;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DanilvarKanji.Application.Auth.Handlers;

public class RequestToChangePasswordHandler
    : IRequestHandler<RequestToChangePasswordCommand, Result<string>>
{
    private readonly IEmailService _emailService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<AppUser> _userManager;
    private readonly IUserRepository _userRepository;

    public RequestToChangePasswordHandler(
        IEmailService emailService,
        UserManager<AppUser> userManager,
        IUnitOfWork unitOfWork,
        IUserRepository userRepository
    )
    {
        _emailService = emailService;
        _userManager = userManager;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }

    public async Task<Result<string>> Handle(
        RequestToChangePasswordCommand request,
        CancellationToken cancellationToken
    )
    {
        AppUser? user = await _userManager.FindByEmailAsync(request.Email);

        if (user != null)
        {
            var code = RandGen.Numbers(6);
            _userRepository.CreateEmailCode(new EmailCode(user.Email!, code));
            if (await _unitOfWork.CompleteAsync())
            {
                await _emailService.SendEmailAsync(
                    request.Email,
                    "Change password",
                    $"Enter this code to change password: {code}"
                );

                return Result.Success(request.Email);
            }

            return Result.Failure<string>(General.UnProcessableRequest);
        }

        return Result.Failure<string>(General.UnProcessableRequest);
    }
}