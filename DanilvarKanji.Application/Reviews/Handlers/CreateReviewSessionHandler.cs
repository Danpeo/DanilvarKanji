using DanilvarKanji.Application.Reviews.Commands;
using DanilvarKanji.Application.Reviews.Events;
using DanilvarKanji.Domain.Errors;
using DanilvarKanji.Domain.Primitives;
using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Infrastructure.Data;
using DanilvarKanji.Shared.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DanilvarKanji.Application.Reviews.Handlers;

public class CreateReviewSessionHandler : IRequestHandler<CreateReviewSessionCommand, Result<string>>
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IExerciseRepository _exerciseRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublisher _publisher;
    private readonly ILogger<CreateReviewSessionHandler> _logger;

    public CreateReviewSessionHandler(IReviewRepository reviewRepository, IUnitOfWork unitOfWork,
        IExerciseRepository exerciseRepository, IPublisher publisher, ILogger<CreateReviewSessionHandler> logger)
    {
        _reviewRepository = reviewRepository;
        _unitOfWork = unitOfWork;
        _exerciseRepository = exerciseRepository;
        _publisher = publisher;
        _logger = logger;
    }

    public async Task<Result<string>> Handle(CreateReviewSessionCommand request, CancellationToken cancellationToken)
    {
        var exercises = new List<Exercise>();

        foreach (var id in request.ExerciseIds)
        {
            Exercise? exercise = await _exerciseRepository.GetAsync(id, request.AppUser);

            if (exercise != null)
                exercises.Add(exercise);
        }

        var reviewSession = new ReviewSession(exercises, request.AppUser);

        _reviewRepository.Create(reviewSession);

        if (await _unitOfWork.CompleteAsync())
        {
            log(LogType.Info);

            await _publisher.Publish(
                new CompletedReviewDomainEvent(reviewSession.Exercises, reviewSession.AppUser,
                    reviewSession.ReviewDataTime), cancellationToken);
            return Result.Success(reviewSession.Id);
        }

        log(LogType.Error);

        return Result.Failure<string>(General.UnProcessableRequest);

        void log(LogType type)
        {
            if (type == LogType.Error)
            {
                _logger.LogError("Failed to create Review Session: {@exercises}, {@appUser}, {@dt}",
                    exercises,
                    request.AppUser,
                    reviewSession!.ReviewDataTime);
            }
            else if (type == LogType.Info)
            {
                _logger.LogInformation("Created Review Session: {@exercises}, {@appUser}, {@dt}",
                    exercises,
                    request.AppUser,
                    reviewSession!.ReviewDataTime);
            }
        }
    }
}