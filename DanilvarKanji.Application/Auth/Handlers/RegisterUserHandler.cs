using AutoMapper;
using DanilvarKanji.Application.Auth.Commands;
using DanilvarKanji.Domain.Entities;
using DanilvarKanji.Domain.Errors;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Domain.Shared.Entities;
using DanilvarKanji.Domain.Shared.Enumerations;
using DanilvarKanji.Infrastructure.Data;
using DanilvarKanji.Infrastructure.Emails;
using DVar.RandCreds;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DanilvarKanji.Application.Auth.Handlers;

// ReSharper disable once UnusedType.Global
public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, IdentityResult>
{
    private readonly IEmailService _emailService;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<AppUser> _userManager;
    private readonly IUserRepository _userRepository;

    public RegisterUserHandler(
        IUserRepository userRepository,
        IMapper mapper,
        UserManager<AppUser> userManager,
        IEmailService emailService,
        IUnitOfWork unitOfWork
    )
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _userManager = userManager;
        _emailService = emailService;
        _unitOfWork = unitOfWork;
    }

    public async Task<IdentityResult> Handle(
        RegisterUserCommand request,
        CancellationToken cancellationToken
    )
    {
        if (await _userRepository.ExistByEmail(request.Email))
            return IdentityResult.Failed(Identity.EmailNotUnique);

        if (request.Password != request.PasswordRepeat)
            return IdentityResult.Failed(Identity.PasswordsDontMatch);

        var user = _mapper.Map<AppUser>(request);

        if (!await _userRepository.AnyExistAsync())
        {
            user.Role = UserRole.SuperAdmin;
        }
        
        IdentityResult result = await _userManager.CreateAsync(user, request.Password);
        if (result.Succeeded)
        {
            var code = RandGen.Numbers(6);
            _userRepository.CreateEmailCode(new EmailCode(user.Email!, code));
            if (await _unitOfWork.CompleteAsync())
                await _emailService.SendEmailAsync(
                    request.Email,
                    "Confirm your email",
                    $"Confirmation code: {code}"
                );
        }

        return result;
    }
}