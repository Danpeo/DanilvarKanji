using DanilvarKanji.Application.Exercises.Queries;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Domain.Shared.Entities;
using MediatR;

namespace DanilvarKanji.Application.Exercises.Handlers;

// ReSharper disable once UnusedType.Global
public class GetExerciseHandler : IRequestHandler<GetExerciseQuery, Exercise?>
{
  private readonly IExerciseRepository _exerciseRepository;

  public GetExerciseHandler(IExerciseRepository exerciseRepository)
  {
    _exerciseRepository = exerciseRepository;
  }

  public async Task<Exercise?> Handle(GetExerciseQuery request, CancellationToken cancellationToken)
  {
    if (await _exerciseRepository.Exist(request.Id, request.AppUser))
      return await _exerciseRepository.GetAsync(request.Id, request.AppUser);

    return null;
  }
}