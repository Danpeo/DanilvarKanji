using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DanilvarKanji.Application.Auth.Commands;

public record ConfirmEmailCommand(string UserEmail, string ConfirmationCode) : IRequest<IdentityResult>;