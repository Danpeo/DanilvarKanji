using DanilvarKanji.Application.Users.Commands;
using DanilvarKanji.Domain.Errors;
using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Infrastructure.Data;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DanilvarKanji.Application.Users.Handlers;

public class UpdateUserXpHandler : IRequestHandler<UpdateUserXpCommand, Result<string>>
{
  private readonly ILogger<UpdateUserXpHandler> _logger;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IUserRepository _userRepository;

  public UpdateUserXpHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    ILogger<UpdateUserXpHandler> logger
  )
  {
    _userRepository = userRepository;
    _unitOfWork = unitOfWork;
    _logger = logger;
  }

  public async Task<Result<string>> Handle(
    UpdateUserXpCommand request,
    CancellationToken cancellationToken
  )
  {
    await _userRepository.UpdateUserXpAsync(request.Xp, request.Email);
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
        _logger.LogInformation("UPDATE user XP: {@request}", request);
      else if (logLevel == LogLevel.Error)
        _logger.LogInformation("UPDATE FAILED for user XP: {@request}", request);
    }
  }
}