using DanilvarKanji.Application.Exercises.Queries;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Domain.Shared.Entities;
using MediatR;

namespace DanilvarKanji.Application.Exercises.Handlers;

// ReSharper disable once UnusedType.Global
public class ListExercisesHandler : IRequestHandler<ListExercisesQuery, IEnumerable<Exercise>>
{
    private readonly IExerciseRepository _exerciseRepository;

    public ListExercisesHandler(IExerciseRepository exerciseRepository)
    {
        _exerciseRepository = exerciseRepository;
    }

    public async Task<IEnumerable<Exercise>> Handle(ListExercisesQuery request, CancellationToken cancellationToken)
    {
        if (await _exerciseRepository.AnyExist())
            return await _exerciseRepository.ListAsync(request.PaginationParams, request.AppUser);

        return Enumerable.Empty<Exercise>();
    }
}