using Danilvar;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Domain.Shared.Entities;
using DanilvarKanji.Domain.Shared.Enumerations;
using DanilvarKanji.Domain.Shared.Params;
using DanilvarKanji.Infrastructure.Data;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DanilvarKanji.Application.Reviews.Events;

public class CompletedReviewEventHandler : INotificationHandler<CompletedReviewDomainEvent>
{
  private readonly ICharacterLearningRepository _characterLearningRepository;
  private readonly CharacterLearningSettings _learningSettings;
  private readonly ILogger<CompletedReviewEventHandler> _logger;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IUserRepository _userRepository;

  public CompletedReviewEventHandler(
    ILogger<CompletedReviewEventHandler> logger,
    ICharacterLearningRepository characterLearningRepository,
    IUnitOfWork unitOfWork,
    IOptions<CharacterLearningSettings> learningSettings,
    IUserRepository userRepository
  )
  {
    _logger = logger;
    _characterLearningRepository = characterLearningRepository;
    _unitOfWork = unitOfWork;
    _userRepository = userRepository;
    _learningSettings = learningSettings.Value;
  }

  public async Task Handle(
    CompletedReviewDomainEvent notification,
    CancellationToken cancellationToken
  )
  {
    if (!notification.Exercises.Any())
      return;

    var characterId = notification.Exercises.First().Character.Id;
    var isCorrect = notification.Exercises.All(e => e.IsCorrect);

    CharacterLearning? characterLearning = await _characterLearningRepository.GetByCharacterIdAsync(
      characterId,
      notification.AppUser
    );

    if (characterLearning is null)
      return;

    _logger.LogInformation("Character Learning: {@characterLearning}", characterLearning);

    updateCharacterLearning();
    await updateUserXp();

    _characterLearningRepository.UpdateCharacterLearning(characterLearning);

    await _unitOfWork.CompleteAsync();

    _logger.LogInformation("UPDATED Character Learning: {@characterLearning}", characterLearning);
    _logger.LogInformation("Completed Review: {@notification}", notification);

    return;

    void updateCharacterLearning()
    {
      characterLearning.LastReviewWasCorrect = isCorrect;
      characterLearning.LastReviewDateTime = notification.ReviewDataTime;

      if (!isCorrect)
      {
        characterLearning.NextReviewDateTime = DateTime.UtcNow.AddMinutes(
          _learningSettings.ShiftExerciseDateAfterFailInMinutes
        );
        return;
      }

      characterLearning.LearningLevelValue += _learningSettings.PointAfterCorrectExercise;
      updateNextReviewDate();

      if (characterLearning.LearningLevelValue >= _learningSettings.MaxLearningRate)
      {
        characterLearning.LearningLevelValue = _learningSettings.MaxLearningRate;
        characterLearning.LearningState = LearningState.LearnedForSomeTime;
        characterLearning.LearnedCount += 1;

        if (characterLearning.LearnedCount >= _learningSettings.CompletelyLearnedAfter)
          characterLearning.LearningState = LearningState.CompletelyLearned;
      }
    }

    void updateNextReviewDate()
    {
      var learningProgress = characterLearning.LearningLevelValue;

      var learningPercent = MathUtils.calcPercentage(
        learningProgress,
        _learningSettings.MaxLearningRate
      );

      var shift = _learningSettings.InitRepeatingShiftHrs;
      var nextShiftMofifier = _learningSettings.NextShiftModifier;

      for (var percent = 10; percent <= 100; percent += 10)
      {
        if (learningPercent <= percent)
        {
          characterLearning.NextReviewDateTime = DateTime.UtcNow.AddHours(shift);
          break;
        }

        shift *= nextShiftMofifier;
      }
    }

    async Task updateUserXp()
    {
      var xp = notification.AppUser.XP;
      if (isCorrect)
        xp += _learningSettings.NormalXp;
      else
        xp += _learningSettings.MinXp;

      await _userRepository.UpdateUserXpAsync(xp, notification.AppUser.Email!);
    }
  }
}