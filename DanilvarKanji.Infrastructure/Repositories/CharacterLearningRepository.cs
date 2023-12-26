using DanilvarKanji.Domain.Entities;
using DanilvarKanji.Domain.Enumerations;
using DanilvarKanji.Domain.Params;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Infrastructure.Common;
using DanilvarKanji.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DanilvarKanji.Infrastructure.Repositories;

public class CharacterLearningRepository : ICharacterLearningRepository
{
    private readonly ApplicationDbContext _context;

    public CharacterLearningRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Create(CharacterLearning characterLearning) =>
        _context.CharacterLearnings.Add(characterLearning);

    public async Task<CharacterLearning?> GetAsync(string id, AppUser user)
    {
        return await GetCharacterLearningsWithRelatedData()
            .FirstOrDefaultAsync(x => x.Id == id && x.AppUser == user);
    }

    public async Task<IEnumerable<CharacterLearning>> ListLearnQueueAsync(PaginationParams? paginationParams,
        AppUser user,
        JlptLevel jlptLevel = JlptLevel.N5)
    {
        return await GetCharacterLearningsWithRelatedData()
            .Where(x => x.AppUser == user)
            .Where(x => x.Character.JlptLevel >= jlptLevel)
            .Where(x => x.LearningState == LearningState.NotLearned)
            .OrderBy(x => x.Character.JlptLevel)
            .ThenBy(x => x.Character.CharacterType)
            .ThenBy(x => x.Character.Definition)
            .ToListAsync();
    }

    public async Task<IEnumerable<CharacterLearning>> ListReviewQueueAsync(PaginationParams? paginationParams,
        AppUser user)
    {
        var characters = await GetReviewQueue(user).ToListAsync();

        return paginationParams is not null ? Paginator.Paginate(characters, paginationParams) : characters;
    }

    public async Task<CharacterLearning?> GetNextInReviewQueue(AppUser appUser) =>
        await GetReviewQueue(appUser).FirstOrDefaultAsync();

    public Task<bool> AnyExist()
        => _context.CharacterLearnings.AnyAsync();

    public Task<bool> AnyToReview(AppUser appUser)
        => _context.CharacterLearnings.AnyAsync(x => x.AppUser == appUser);

    public Task<bool> Exist(string requestId, AppUser user) =>
        _context.CharacterLearnings.AnyAsync(x => x.Id == requestId && x.AppUser == user);

    private IQueryable<CharacterLearning> GetCharacterLearningsWithRelatedData()
    {
        var characters = _context.CharacterLearnings
            .AsSplitQuery()
            .Include(x => x.Character)
            .Include(x => x.LearningProgress);

        return characters.OrderByDescending(x => x.LearningState);
    }

    private IOrderedQueryable<CharacterLearning> GetReviewQueue(AppUser appUser)
    {
        IOrderedQueryable<CharacterLearning> characters = _context.CharacterLearnings
            .AsSplitQuery()
            .Include(c => c.Character)
            .Include(c => c.LearningProgress)
            .Where(c => c.AppUser == appUser && c.LearningState == LearningState.Learning &&
                        c.LearningState != LearningState.Skipped)
            .OrderBy(c => c.LearningProgress)
            .ThenBy(c => c.LastReviewDateTime);

        return characters;
    }
}