using AutoMapper;
using DanilvarKanji.Application.Reviews.Commands;
using DanilvarKanji.Domain.Entities;
using DanilvarKanji.Domain.Entities.Exercises;
using DanilvarKanji.Domain.Errors;
using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Infrastructure.Data;
using MediatR;

namespace DanilvarKanji.Application.Reviews.Handlers;

public class CreateReviewSessionHandler : IRequestHandler<CreateReviewSessionCommand, Result<string>>
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IExerciseRepository _exerciseRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateReviewSessionHandler(IReviewRepository reviewRepository, IUnitOfWork unitOfWork, IMapper mapper,
        IExerciseRepository exerciseRepository)
    {
        _reviewRepository = reviewRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _exerciseRepository = exerciseRepository;
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
            return Result.Success(reviewSession.Id);

        return Result.Failure<string>(General.UnProcessableRequest);
    }
}