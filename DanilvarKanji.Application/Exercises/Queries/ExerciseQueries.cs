using DanilvarKanji.Shared.Domain.Entities;
using DanilvarKanji.Shared.Domain.Params;
using MediatR;

namespace DanilvarKanji.Application.Exercises.Queries;

public record ListExercisesQuery
    (PaginationParams? PaginationParams, AppUser AppUser) : IRequest<IEnumerable<Exercise>>;

public record GetExerciseQuery(string Id, AppUser AppUser) : IRequest<Exercise?>;