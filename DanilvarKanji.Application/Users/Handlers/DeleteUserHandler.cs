using DanilvarKanji.Application.Users.Commands;
using DanilvarKanji.Domain.Errors;
using DanilvarKanji.Domain.Persistance;
using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Infrastructure.Data;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DanilvarKanji.Application.Users.Handlers;

public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, Result<string>>
{
    private readonly ILogger<DeleteUserHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;

    public DeleteUserHandler(
        IUserRepository userRepository,
        ILogger<DeleteUserHandler> logger,
        IUnitOfWork unitOfWork
    )
    {
        _userRepository = userRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(
        DeleteUserCommand request,
        CancellationToken cancellationToken
    )
    {
        if (await _userRepository.ExistByEmail(request.Email))
        {
            await _userRepository.DeleteAsync(request.Email);

            if (await _unitOfWork.CompleteAsync())
            {
                _logger.LogInformation("DELETED User: {@user}", request.Email);

                return Result.Success(request.Email);
            }
        }

        _logger.LogError("FAILED to DELETE User: {@user}", request.Email);
        return Result.Failure<string>(General.NotFound("User"));
    }
}