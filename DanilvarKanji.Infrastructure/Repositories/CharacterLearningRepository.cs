using DanilvarKanji.Domain.Entities;
using DanilvarKanji.Domain.Enumerations;
using DanilvarKanji.Domain.Params;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DanilvarKanji.Infrastructure.Repositories;

public class CharacterLearningRepository : ICharacterLearningRepository
{
    private readonly ApplicationDbContext _context;
    private ICharacterLearningRepository _characterLearningRepositoryImplementation;

    public CharacterLearningRepository(ApplicationDbContext context, ICharacterLearningRepository characterLearningRepositoryImplementation)
    {
        _context = context;
        _characterLearningRepositoryImplementation = characterLearningRepositoryImplementation;
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

    public Task<bool> AnyExist()
        => _context.CharacterLearnings.AnyAsync();

    public Task<bool> Exist(string requestId, AppUser user) =>
        _context.CharacterLearnings.AnyAsync(x => x.Id == requestId && x.AppUser == user);

    private IQueryable<CharacterLearning> GetCharacterLearningsWithRelatedData()
    {
        var characters = _context.CharacterLearnings
            .Include(x => x.Character)
            .Include(x => x.LearningProgress);

        return characters.OrderByDescending(x => x.LearningState);
    }
}