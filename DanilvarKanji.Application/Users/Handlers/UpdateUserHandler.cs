using DanilvarKanji.Application.Users.Commands;
using DanilvarKanji.Domain.Errors;
using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Infrastructure.Data;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DanilvarKanji.Application.Users.Handlers;

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, Result<string>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateUserHandler> _logger;

    public UpdateUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, ILogger<UpdateUserHandler> logger)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        await _userRepository.UpdateUserAsync(request.Email, request.NewUserName, request.NewUserRole);

        if (await _unitOfWork.CompleteAsync())
        {
            log(LogLevel.Information);
            return Result.Success(request.Email);
        }

        log(LogLevel.Error);
        return Result.Failure<string>(General.UnProcessableRequest);

        void log(LogLevel logLevel)
        {
            if (logLevel == LogLevel.Information)
                _logger.LogInformation("UPDATE User: {@request}", request);
            else if (logLevel == LogLevel.Error)
                _logger.LogInformation("UPDATE FAILED for User: {@request}", request);
        }
    }
}