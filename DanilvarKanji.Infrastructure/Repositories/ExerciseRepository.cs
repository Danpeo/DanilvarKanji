using DanilvarKanji.Domain.Entities.Exercises;
using DanilvarKanji.Domain.Params;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Infrastructure.Common;
using DanilvarKanji.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DanilvarKanji.Infrastructure.Repositories;


public class ExerciseRepository : IExerciseRepository
{
    private readonly ApplicationDbContext _context;

    public ExerciseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Create(Exercise ex) =>
        _context.Exercises.Add(ex);

    public async Task<IEnumerable<Exercise>> ListAsync(PaginationParams? paginationParams)
    {
        List<Exercise> exercises = await GetExsWithRelatedData()
            .ToListAsync();

        return paginationParams != null ? Paginator.Paginate(exercises, paginationParams) : exercises;
    }

    private IQueryable<Exercise> GetExsWithRelatedData()
    {
        var exs = _context.Exercises
            .AsSplitQuery()
            .Include(x => x.Character)
            .Include(x => x.AppUser);

        return exs.OrderByDescending(x => x.ExcerciseDateTime);
    }
}