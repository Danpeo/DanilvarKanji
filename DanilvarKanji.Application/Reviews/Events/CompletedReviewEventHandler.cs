using Danilvar;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Infrastructure.Data;
using DanilvarKanji.Shared.Domain.Entities;
using DanilvarKanji.Shared.Domain.Enumerations;
using DanilvarKanji.Shared.Domain.Settings;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DanilvarKanji.Application.Reviews.Events;

public class CompletedReviewEventHandler : INotificationHandler<CompletedReviewDomainEvent>
{
    private readonly ICharacterLearningRepository _characterLearningRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CompletedReviewEventHandler> _logger;
    private readonly CharacterLearningSettings _learningSettings;

    public CompletedReviewEventHandler(
        ILogger<CompletedReviewEventHandler> logger,
        ICharacterLearningRepository characterLearningRepository,
        IUnitOfWork unitOfWork,
        IOptions<CharacterLearningSettings> learningSettings)
    {
        _logger = logger;
        _characterLearningRepository = characterLearningRepository;
        _unitOfWork = unitOfWork;
        _learningSettings = learningSettings.Value;
    }

    public async Task Handle(CompletedReviewDomainEvent notification, CancellationToken cancellationToken)
    {
        if (!notification.Exercises.Any())
            return;

        string characterId = notification.Exercises.First().Character.Id;

        bool isCorrect = notification.Exercises.All(e => e.IsCorrect);

        CharacterLearning? characterLearning = await _characterLearningRepository
            .GetByCharacterIdAsync(characterId, notification.AppUser);

        if (characterLearning is null)
            return;

        _logger.LogInformation("Character Learning: {@characterLearning}", characterLearning);

        updateCharacterLearning();

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
                characterLearning.NextReviewDateTime =
                    DateTime.UtcNow.AddMinutes(_learningSettings.ShiftExerciseDateAfterFailInMinutes);
                return;
            }

            characterLearning.LearningProgress.Value += _learningSettings.PointAfterCorrectExercise;
            updateNextReviewDate();

            if (characterLearning.LearningProgress.Value >= _learningSettings.MaxLearningRate)
            {
                characterLearning.LearningProgress.Value = _learningSettings.MaxLearningRate;
                characterLearning.LearningState = LearningState.LearnedForSomeTime;
                characterLearning.LearnedCount += 1;

                if (characterLearning.LearnedCount >= _learningSettings.CompletelyLearnedAfter)
                    characterLearning.LearningState = LearningState.CompletelyLearned;
            }
        }

        void updateNextReviewDate()
        {
            float learningProgress = characterLearning.LearningProgress.Value;
            double learningPercent = MathUtils.calcPercentage
            (
                learningProgress,
                _learningSettings.MaxLearningRate
            );

            float shift = _learningSettings.InitRepeatingShiftHrs;
            float nextShiftMofifier = _learningSettings.NextShiftModifier;
            
            for (int percent = 10; percent <= 100; percent += 10)
            {
                if (learningPercent <= percent)
                {
                    characterLearning.NextReviewDateTime = DateTime.UtcNow.AddHours(shift);
                    break;
                }
                shift *= nextShiftMofifier;
            }

            /*characterLearning.NextReviewDateTime = learningPercent switch
            {
                <= 10 => DateTime.UtcNow.AddHours(12),
                <= 20 => DateTime.UtcNow.AddHours(18),
                <= 30 => DateTime.UtcNow.AddHours(27),
                <= 40 => DateTime.UtcNow.AddHours(40.5),
                <= 50 => DateTime.UtcNow.AddHours(136.68),
                <= 60 => DateTime.UtcNow.AddHours(205.03),
                <= 70 => DateTime.UtcNow.AddHours(307.54),
                <= 80 => DateTime.UtcNow.AddHours(461.32),
                <= 90 => DateTime.UtcNow.AddHours(691.98),
                <= 100 => DateTime.UtcNow.AddHours(1037),
                _ => characterLearning.NextReviewDateTime
            };*/
        }
    }
}