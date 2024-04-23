using DanilvarKanji.Domain.Shared.Enumerations;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DanilvarKanji.Application.Auth.Commands;

public record RegisterUserCommand(
    string UserName,
    JlptLevel JlptLevel,
    string Email,
    string Password,
    string PasswordRepeat) : IRequest<IdentityResult>;