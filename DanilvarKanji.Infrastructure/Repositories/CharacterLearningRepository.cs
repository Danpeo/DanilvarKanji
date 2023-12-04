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

    public CharacterLearningRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public void Create(CharacterLearning characterLearning) =>
        _context.CharacterLearnings.Add(characterLearning);

    public async Task<IEnumerable<CharacterLearning>> ListLearnQueueAsync(PaginationParams? paginationParams,
        AppUser user,
        JlptLevel jlptLevel = JlptLevel.N5)
    {
        return await GetCharacterLearningsWithRelatedData(paginationParams)
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


    private IQueryable<CharacterLearning> GetCharacterLearningsWithRelatedData(
        PaginationParams? paginationParams = null)
    {
        var characters = _context.CharacterLearnings
            .Include(x => x.Character)
            .Include(x => x.LearningProgress);

        if (paginationParams != null && paginationParams.PageNumber != 0 && paginationParams.PageSize != 0)
        {
            return characters
                .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                .Take(paginationParams.PageSize);
        }

        return characters;
    }
}