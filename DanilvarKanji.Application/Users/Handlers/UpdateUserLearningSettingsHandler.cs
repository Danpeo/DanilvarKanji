using DanilvarKanji.Application.Users.Commands;
using DanilvarKanji.Domain.Errors;
using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Infrastructure.Data;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DanilvarKanji.Application.Users.Handlers;

public class UpdateUserLearningSettingsHandler
  : IRequestHandler<UpdateUserLearningSettingsCommand, Result<string>>
{
  private readonly ILogger<UpdateUserLearningSettingsHandler> _logger;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IUserRepository _userRepository;

  public UpdateUserLearningSettingsHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    ILogger<UpdateUserLearningSettingsHandler> logger
  )
  {
    _userRepository = userRepository;
    _unitOfWork = unitOfWork;
    _logger = logger;
  }

  public async Task<Result<string>> Handle(
    UpdateUserLearningSettingsCommand request,
    CancellationToken cancellationToken
  )
  {
    await _userRepository.UpdateUserLearningSettingsAsync(request.Email, request.LearningSettings);

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
        _logger.LogInformation("UPDATE Learning Settings: {@request}", request);
      else if (logLevel == LogLevel.Error)
        _logger.LogInformation("UPDATE FAILED for Learning Settings: {@request}", request);
    }
  }
}