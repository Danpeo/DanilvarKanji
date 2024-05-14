using DanilvarKanji.Application.Reviews.Commands;
using DanilvarKanji.Application.Reviews.Events;
using DanilvarKanji.Domain.Errors;
using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Domain.Shared.Entities;
using DanilvarKanji.Infrastructure.Data;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DanilvarKanji.Application.Reviews.Handlers;

public class CreateReviewSessionHandler
    : IRequestHandler<CreateReviewSessionCommand, Result<string>>
{
    private readonly IExerciseRepository _exerciseRepository;
    private readonly ILogger<CreateReviewSessionHandler> _logger;
    private readonly IPublisher _publisher;
    private readonly IReviewRepository _reviewRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateReviewSessionHandler(
        IReviewRepository reviewRepository,
        IUnitOfWork unitOfWork,
        IExerciseRepository exerciseRepository,
        IPublisher publisher,
        ILogger<CreateReviewSessionHandler> logger
    )
    {
        _reviewRepository = reviewRepository;
        _unitOfWork = unitOfWork;
        _exerciseRepository = exerciseRepository;
        _publisher = publisher;
        _logger = logger;
    }

    public async Task<Result<string>> Handle(
        CreateReviewSessionCommand request,
        CancellationToken cancellationToken
    )
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
            log(LogLevel.Information);

            await _publisher.Publish(
                new CompletedReviewDomainEvent(
                    reviewSession.Exercises,
                    reviewSession.AppUser,
                    reviewSession.ReviewDataTime
                ),
                cancellationToken
            );
            return Result.Success(reviewSession.Id);
        }

        log(LogLevel.Error);

        return Result.Failure<string>(General.UnProcessableRequest);

        void log(LogLevel logLevel)
        {
            if (logLevel == LogLevel.Error)
                _logger.LogError(
                    "Failed to create Review Session: {@exercises}, {@appUser}, {@dt}",
                    exercises,
                    request.AppUser,
                    reviewSession!.ReviewDataTime
                );
            else if (logLevel == LogLevel.Information)
                _logger.LogInformation(
                    "Created Review Session: {@exercises}, {@appUser}, {@dt}",
                    exercises,
                    request.AppUser,
                    reviewSession!.ReviewDataTime
                );
        }
    }
}