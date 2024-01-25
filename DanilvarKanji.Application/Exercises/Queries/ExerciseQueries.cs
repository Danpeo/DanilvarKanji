using DanilvarKanji.Domain.Params;
using DanilvarKanji.Shared.Domain.Entities;
using MediatR;

namespace DanilvarKanji.Application.Exercises.Queries;

public record ListExercisesQuery
    (PaginationParams? PaginationParams, AppUser AppUser) : IRequest<IEnumerable<Exercise>>;

public record GetExerciseQuery(string Id, AppUser AppUser) : IRequest<Exercise?>;