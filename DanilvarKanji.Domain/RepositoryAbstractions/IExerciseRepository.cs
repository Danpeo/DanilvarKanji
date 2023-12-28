using DanilvarKanji.Domain.Entities.Exercises;
using DanilvarKanji.Domain.Params;

namespace DanilvarKanji.Domain.RepositoryAbstractions;

public interface IExerciseRepository
{
    void Create(Exercise ex);
    Task<IEnumerable<Exercise>> ListAsync(PaginationParams? paginationParams);
}