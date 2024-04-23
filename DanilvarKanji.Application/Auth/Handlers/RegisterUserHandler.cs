using AutoMapper;
using DanilvarKanji.Application.Auth.Commands;
using DanilvarKanji.Domain.Errors;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Domain.Shared.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DanilvarKanji.Application.Auth.Handlers;

// ReSharper disable once UnusedType.Global
public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, IdentityResult>
{
    private readonly IUserRepository _userRepository;
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;

    public RegisterUserHandler(IUserRepository userRepository, IMapper mapper, UserManager<AppUser> userManager)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<IdentityResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (await _userRepository.ExistByEmail(request.Email))
            return IdentityResult.Failed(Identity.EmailNotUnique);
        
        if (request.Password != request.PasswordRepeat)
            return IdentityResult.Failed(Identity.PasswordsDontMatch);
            
        var user = _mapper.Map<AppUser>(request);

        IdentityResult result = await _userManager.CreateAsync(user, request.Password);
        
        return result;
    }
}