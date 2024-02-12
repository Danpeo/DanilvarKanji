using System.Linq.Expressions;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Infrastructure.Common;
using DanilvarKanji.Infrastructure.Data;
using DanilvarKanji.Shared.Domain.Entities;
using DanilvarKanji.Shared.Domain.Enumerations;
using DanilvarKanji.Shared.Domain.Params;
using DanilvarKanji.Shared.Domain.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DanilvarKanji.Infrastructure.Repositories;

public class CharacterLearningRepository : ICharacterLearningRepository
{
    private readonly ApplicationDbContext _context;
    private readonly CharacterLearningSettings _learningSettings;

    public CharacterLearningRepository(ApplicationDbContext context,
        IOptions<CharacterLearningSettings> learningSettings)
    {
        _context = context;
        _learningSettings = learningSettings.Value;
    }

    public void Create(CharacterLearning characterLearning) =>
        _context.CharacterLearnings.Add(characterLearning);

    public async Task ToggleSkipStateAsync(string id, AppUser user)
    {
        var characterLearning = await _context.CharacterLearnings
            .FirstOrDefaultAsync(x => x.Id == id && x.AppUser == user);

        if (characterLearning is null)
            return;

        if (characterLearning.LearningState == LearningState.Skipped)
            characterLearning.LearningState = LearningState.Learning;
        else
            characterLearning.LearningState = LearningState.Skipped;

        _context.CharacterLearnings.Update(characterLearning);
    }

    public async Task UpdateProgressAsync(string id, AppUser user, bool lastReviewWasCorrect)
    {
        var characterLearning = await _context.CharacterLearnings
            .FirstOrDefaultAsync(x => x.Id == id && x.AppUser == user);

        if (characterLearning is null)
            return;

        characterLearning.LastReviewDateTime = DateTime.UtcNow;
        characterLearning.LastReviewWasCorrect = lastReviewWasCorrect;

        if (lastReviewWasCorrect)
        {
            characterLearning.LearnedCount++;
        }

        _context.CharacterLearnings.Update(characterLearning);
    }

    public async Task UpdateProgressOnCharacterAsync(string characterId, AppUser user, DateTime reviewDateTime,
        bool isCorrect)
    {
        var characterLearning = await _context.CharacterLearnings
            .Include(characterLearning => characterLearning.LearningProgress)
            .FirstOrDefaultAsync(cl => cl.Character.Id == characterId && cl.AppUser == user);

        if (characterLearning is null)
            return;

        characterLearning.LastReviewDateTime = reviewDateTime;
        characterLearning.LastReviewWasCorrect = isCorrect;

        if (characterLearning.LastReviewWasCorrect)
        {
            characterLearning.LearningProgress.Value += _learningSettings.PointAfterCorrectExercise;
        }

        _context.CharacterLearnings.Update(characterLearning);
    }

    public void UpdateCharacterLearning(CharacterLearning characterLearning) =>
        _context.CharacterLearnings.Update(characterLearning);

    public float TestLearningSettings(string message)
    {
        return _learningSettings.MaxLearningRate;
    }

    public async Task<CharacterLearning?> GetAsync(string id, AppUser user)
    {
        return await GetCharacterLearningsWithRelatedData()
            .FirstOrDefaultAsync(x => x.Id == id && x.AppUser == user);
    }

    public async Task<CharacterLearning?> GetByCharacterIdAsync(string id, AppUser user)
    {
        return await GetCharacterLearningsWithRelatedData()
            .FirstOrDefaultAsync(x => x.Character.Id == id && x.AppUser == user);
    }

    public async Task<IEnumerable<CharacterLearning>> ListLearnQueueAsync(PaginationParams? paginationParams,
        AppUser user,
        JlptLevel jlptLevel = JlptLevel.N5)
    {
        var charLearnings = await GetCharacterLearningsWithRelatedData()
            .Where(x => x.AppUser == user)
            .Where(x => x.Character.JlptLevel >= jlptLevel)
            .Where(x => x.LearningState == LearningState.NotLearned)
            .OrderBy(x => x.Character.JlptLevel)
            .ThenBy(x => x.Character.CharacterType)
            .ThenBy(x => x.Character.Definition)
            .ToListAsync();

        return paginationParams != null ? Paginator.Paginate(charLearnings, paginationParams) : charLearnings;
    }

    public async Task<IEnumerable<CharacterLearning>> ListSkippedAsync(PaginationParams? paginationParams, AppUser user)
    {
        var charLearnings = await GetCharacterLearningsWithRelatedData()
            .Where(x => x.AppUser == user)
            .Where(x => x.LearningState == LearningState.Skipped)
            .OrderBy(x => x.Character.JlptLevel)
            .ThenBy(x => x.Character.CharacterType)
            .ThenBy(x => x.Character.Definition)
            .ToListAsync();

        return paginationParams != null ? Paginator.Paginate(charLearnings, paginationParams) : charLearnings;
    }

    public async Task<IEnumerable<CharacterLearning>> ListCurrentReviewQueueAsync
    (
        PaginationParams? paginationParams,
        AppUser user
    )
    {
        var characters = await GetCurrentReviewQueue(user).ToListAsync();

        return paginationParams is not null ? Paginator.Paginate(characters, paginationParams) : characters;
    }

    public async Task<IEnumerable<CharacterLearning>> ListFutureReviewQueueAsync
    (
        PaginationParams? paginationParams,
        AppUser user
    )
    {
        var characters = await GetFutureReviewQueue(user).ToListAsync();

        return paginationParams is not null ? Paginator.Paginate(characters, paginationParams) : characters;
    }

    public async Task<List<string>> GetRandomMeaningsInReviewQueueAsync(string characterId, AppUser user,
        Culture culture, int qty)
    {
        var random = new Random();

        List<string> definitions = await GetFullReviewQueue(user)
            .Where(x => x.Character.Id != characterId)
            .AsSplitQuery()
            .SelectMany(cl => cl.Character.KanjiMeanings!)
            .SelectMany(km => km.Definitions!
                .Where(sd => sd.Culture == culture)
                .Select(sd => sd.Value))
            .ToListAsync();

        var shuffledDefinitions = definitions
            .OrderBy(_ => random.Next())
            .Take(qty)
            .ToList();

        return shuffledDefinitions;
    }

    public async Task<List<string>> GetRandomKunyomisInReviewQueueAsync(string characterId, AppUser user, int qty)
    {
        var random = new Random();

        var kuns = await GetFullReviewQueue(user)
            .Where(x => x.Character.Id != characterId)
            .AsSplitQuery()
            .SelectMany(cl => cl.Character.Kunyomis!)
            .Select(k => k.JapaneseWriting)
            .ToListAsync();

        var shuffledKuns = kuns
            .OrderBy(_ => random.Next())
            .Take(qty)
            .ToList();

        return shuffledKuns;
    }

    public async Task<List<string>> GetRandomOnyomisInReviewQueueAsync(string characterId, AppUser user, int qty)
    {
        var random = new Random();

        var ons = await GetFullReviewQueue(user)
            .Where(x => x.Character.Id != characterId)
            .AsSplitQuery()
            .SelectMany(cl => cl.Character.Onyomis!)
            .Select(k => k.JapaneseWriting)
            .ToListAsync();

        var shuffledOns = ons
            .OrderBy(_ => random.Next())
            .Take(qty)
            .ToList();

        return shuffledOns;
    }

    public async Task<CharacterLearning?> GetNextInReviewQueue(AppUser appUser) =>
        await GetCurrentReviewQueue(appUser).FirstOrDefaultAsync();

    public async ValueTask<bool> AnyExist() =>
        await _context.CharacterLearnings.AnyAsync();

    public async ValueTask<bool> AnyToReview(AppUser appUser) =>
        await _context.CharacterLearnings.AnyAsync(x => x.AppUser == appUser);

    public async ValueTask<bool> Exist(string requestId, AppUser user) =>
        await _context.CharacterLearnings.AnyAsync(x => x.Id == requestId && x.AppUser == user);

    private IQueryable<CharacterLearning> GetCharacterLearningsWithRelatedData()
    {
        var characters = _context.CharacterLearnings
            .AsSplitQuery()
            .Include(x => x.Character)
            .Include(x => x.LearningProgress);

        return characters.OrderByDescending(x => x.LearningState);
    }

    private IOrderedQueryable<CharacterLearning> GetReviewQueue(Expression<Func<CharacterLearning, bool>> condition)
    {
        IOrderedQueryable<CharacterLearning> characters = _context.CharacterLearnings
            .AsSplitQuery()
            .Include(c => c.Character)
            .ThenInclude(ch => ch.KanjiMeanings)
            .Include(c => c.LearningProgress)
            .Where(condition)
            .OrderBy(c => c.LearningState)
            .ThenBy(c => c.LastReviewDateTime)
            .ThenBy(c => c.LearningProgress.Value)
            .ThenBy(c => c.LearnedCount)
            .ThenBy(c => c.LastReviewWasCorrect);

        return characters;
    }

    private IOrderedQueryable<CharacterLearning> GetCurrentReviewQueue(AppUser appUser)
    {
        return GetReviewQueue(learning =>
            learning.AppUser == appUser &&
            learning.NextReviewDateTime <= DateTime.Today &&
            learning.LearningState == LearningState.Learning &&
            learning.LearningState != LearningState.Skipped &&
            learning.LearningProgress.Value < _learningSettings.MaxLearningRate
        );
    }

    private IOrderedQueryable<CharacterLearning> GetFutureReviewQueue(AppUser appUser)
    {
        return GetReviewQueue(learning =>
            learning.AppUser == appUser &&
            learning.NextReviewDateTime > DateTime.Today &&
            learning.LearningState == LearningState.Learning &&
            learning.LearningState != LearningState.Skipped &&
            learning.LearningProgress.Value < _learningSettings.MaxLearningRate
        );
    }
    
    private IOrderedQueryable<CharacterLearning> GetFullReviewQueue(AppUser appUser)
    {
        return GetReviewQueue(learning =>
            learning.AppUser == appUser &&
            learning.LearningState == LearningState.Learning &&
            learning.LearningState != LearningState.Skipped &&
            learning.LearningProgress.Value < _learningSettings.MaxLearningRate
        );
    }
}