using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Domain.Shared.Entities;
using DanilvarKanji.Domain.Shared.Params;
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

    public async Task<IEnumerable<Exercise>> ListAsync(PaginationParams? paginationParams, AppUser user)
    {
        List<Exercise> exercises = await GetExsWithRelatedData()
            .Where(x => x.AppUser == user)
            .ToListAsync();

        return paginationParams != null ? Paginator.Paginate(exercises, paginationParams) : exercises;
    }

    public async Task<Exercise?> GetAsync(string id, AppUser user) =>
        await GetExsWithRelatedData()
            .FirstOrDefaultAsync(x => x.Id == id && x.AppUser == user);

    public Task<bool> AnyExist()
        => _context.Exercises.AnyAsync();

    public Task<bool> Exist(string id, AppUser user) =>
        _context.Exercises.AnyAsync(x => x.Id == id && x.AppUser == user);

    public Task<bool> AnyToReview(AppUser appUser)
        => _context.Exercises.AnyAsync(x => x.AppUser == appUser);

    private IQueryable<Exercise> GetExsWithRelatedData()
    {
        var exs = _context.Exercises
            .AsSplitQuery()
            .Include(x => x.Character)
            .Include(x => x.AppUser);

        return exs.OrderByDescending(x => x.ExcerciseDateTime);
    }
}