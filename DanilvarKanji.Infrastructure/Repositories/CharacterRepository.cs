using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Domain.Shared.Entities;
using DanilvarKanji.Domain.Shared.Enumerations;
using DanilvarKanji.Domain.Shared.Params;
using DanilvarKanji.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DanilvarKanji.Infrastructure.Repositories;

public class CharacterRepository : ICharacterRepository
{
    private readonly ApplicationDbContext _context;

    public CharacterRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Create(Character character)
    {
        _context.Characters.Add(character);
    }

    public async Task<IEnumerable<Character>> ListAsync(PaginationParams paginationParams)
    {
        var characters = await GetCharactersWithRelatedData().ToListAsync();

        return characters.Paginate(paginationParams);
    }

    public async Task<IEnumerable<Character>> ListLearnQueueAsync(PaginationParams paginationParams,
        AppUser user,
        JlptLevel jlptLevel = JlptLevel.N5)
    {
        var characters = await GetLearnQueue(user, jlptLevel).ToListAsync();

        return characters.Paginate(paginationParams);
    }

    public async Task<Character?> GetNextInLearnQueueAsync(AppUser user)
    {
        return await GetLearnQueue(user, user.JlptLevel).FirstOrDefaultAsync();
    }

    public async Task<Character?> GetAsync(string id)
    {
        return await GetCharactersWithRelatedData().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task UpdateAsync(string id, Character character)
    {
        Character? characterToUpdate = await _context.Characters.FirstOrDefaultAsync(x => x.Id == id);

        if (characterToUpdate is null)
            return;

        characterToUpdate.Definition = character.Definition;
        characterToUpdate.StrokeCount = character.StrokeCount;
        characterToUpdate.JlptLevel = character.JlptLevel;
        characterToUpdate.CharacterType = character.CharacterType;
        characterToUpdate.KanjiMeanings = character.KanjiMeanings;
        characterToUpdate.Kunyomis = character.Kunyomis;
        characterToUpdate.Onyomis = character.Onyomis;
        characterToUpdate.Mnemonics = character.Mnemonics;

        _context.Characters.Update(character);
    }

    public async Task DeleteAsync(string id)
    {
        Character? character = await GetAsync(id);
        _context.Characters.Remove(character!);
    }

    public void DeleteRange(IEnumerable<string> ids)
    {
        var characters = _context.Characters.Where(c => ids.Contains(c.Id));
        _context.Characters.RemoveRange(characters);
    }

    public void DeleteAll()
    {
        _context.Characters.RemoveRange(_context.Characters);
    }

    public async ValueTask<bool> ExistAsync(string id)
    {
        return await _context.Characters.AnyAsync(x => x.Id == id);
    }

    public async ValueTask<bool> AnyExistAsync()
    {
        return await _context.Characters.AnyAsync();
    }

    public async ValueTask<bool> AnyInLearnQueueAsync(AppUser user)
    {
        return await GetLearnQueue(user, user.JlptLevel).AnyAsync();
    }

    public async Task<IEnumerable<string>> GetKanjiMeaningsByPriority(
        string characterId,
        int takeQty,
        Culture culture
    )
    {
        Character? character = await GetAsync(characterId);
        if (character != null)
            if (character.KanjiMeanings != null)
                return character
                    .KanjiMeanings.Where(x => x.Definitions != null)
                    .OrderByDescending(x => x.Priority)
                    .Take(takeQty)
                    .SelectMany(x => x.Definitions)
                    .Where(d => d.Culture == culture)
                    .Select(x => x.Value)
                    .ToList();

        return Enumerable.Empty<string>();
    }

    public string? GetRandomMeaningFromCharacter(string id, Culture culture)
    {
        var random = new Random();

        var kanjiMeanings = _context
            .Characters.Include(character => character.KanjiMeanings)
            .ThenInclude(kanjiMeaning => kanjiMeaning.Definitions)
            .FirstOrDefault(x => x.Id == id)
            ?.KanjiMeanings;

        if (kanjiMeanings != null)
        {
            IEnumerable<string?> meanings = kanjiMeanings.SelectMany(km =>
                km.Definitions.Where(d => d.Culture == culture).Select(km => km.Value)
            );

            var randomMeanings = meanings.OrderBy(_ => random.Next()).Take(1).ToList();

            return randomMeanings[0];
        }

        return default;
    }

    public string? GetRandomKunyomiFromCharacter(string id)
    {
        var random = new Random();

        var kunReadings = _context
            .Characters.Include(character => character.Kunyomis)
            .FirstOrDefault(x => x.Id == id)
            ?.Kunyomis;

        if (kunReadings != null)
        {
            var randomKunyomi = kunReadings
                .Select(k => k.JapaneseWriting)
                .OrderBy(_ => random.Next())
                .Take(1)
                .ToList();

            return randomKunyomi[0];
        }

        return default;
    }

    public string? GetRandomOnyomiFromCharacter(string id)
    {
        var random = new Random();

        var onReadings = _context
            .Characters.Include(character => character.Onyomis)
            .FirstOrDefault(x => x.Id == id)
            ?.Onyomis;

        if (onReadings != null)
        {
            var randomOnyomi = onReadings
                .Select(k => k.JapaneseWriting)
                .OrderBy(_ => random.Next())
                .Take(1)
                .ToList();

            return randomOnyomi[0];
        }

        return default;
    }

    public async Task<List<string>> GetRandomMeaningsInLearnQueueAsync(
        AppUser user,
        Culture culture,
        int qty
    )
    {
        var random = new Random();

        var definitions = await GetLearnQueue(user, user.JlptLevel)
            .AsSplitQuery()
            .SelectMany(c => c.KanjiMeanings)
            .SelectMany(km => km.Definitions!.Where(sd => sd.Culture == culture).Select(sd => sd.Value))
            .ToListAsync();

        var shuffledDefinitions = definitions.OrderBy(_ => random.Next()).Take(qty).ToList();

        return shuffledDefinitions;
    }

    public async Task<List<string>> GetRandomKunReadingsInLearnQueueAsync(AppUser user, int qty)
    {
        var random = new Random();

        var kuns = await GetLearnQueue(user, user.JlptLevel)
            .AsSplitQuery()
            .SelectMany(c => c.Kunyomis)
            .Select(k => k.JapaneseWriting)
            .ToListAsync();

        var shuffledKuns = kuns.OrderBy(_ => random.Next()).Take(qty).ToList();

        return shuffledKuns;
    }

    public async Task<List<string>> GetRandomOnReadingsInLearnQueueAsync(AppUser user, int qty)
    {
        var random = new Random();

        var kuns = await GetLearnQueue(user, user.JlptLevel)
            .AsSplitQuery()
            .SelectMany(c => c.Onyomis)
            .Select(k => k.JapaneseWriting)
            .ToListAsync();

        var shuffledKuns = kuns.OrderBy(_ => random.Next()).Take(qty).ToList();

        return shuffledKuns;
    }

    public async Task<IEnumerable<Character>> SearchAsync(string searchTerm)
    {
        return await GetCharactersWithRelatedData()
            .Where(x =>
                x.Onyomis != null
                && x.Kunyomis != null
                && x.KanjiMeanings != null
                && x.Definition != null
                && (
                    EF.Functions.ILike(x.Definition, $"%{searchTerm}%")
                    || x.KanjiMeanings.Any(km =>
                        km.Definitions != null
                        && km.Definitions.Any(d => EF.Functions.ILike(d.Value, $"%{searchTerm}%"))
                    )
                    || x.Kunyomis.Any(k =>
                        EF.Functions.ILike(k.JapaneseWriting, $"%{searchTerm}%")
                        || EF.Functions.ILike(k.Romaji, $"%{searchTerm}%")
                    )
                    || x.Onyomis.Any(o =>
                        o.Romaji != null
                        && (
                            EF.Functions.ILike(o.JapaneseWriting, $"%{searchTerm}%")
                            || EF.Functions.ILike(o.Romaji, $"%{searchTerm}%")
                        )
                    )
                )
            )
            .ToListAsync();
    }

    private IQueryable<Character> GetCharactersWithRelatedData()
    {
        var characters = _context
            .Characters.AsSplitQuery()
            .Include(c => c.Mnemonics)
            .Include(c => c.KanjiMeanings)
            .ThenInclude(km => km.Definitions)
            .Include(c => c.Kunyomis)
            .Include(c => c.Onyomis);

        return characters.OrderByDescending(x => x.Definition);
    }

    private IOrderedQueryable<Character> GetLearnQueue(AppUser user, JlptLevel jlptLevel)
    {
        var characterLearnings = _context.CharacterLearnings;

        var characters = _context
            .Characters.Where(character =>
                character.JlptLevel >= jlptLevel
                && !characterLearnings.Any(cl =>
                    cl.Character.Id == character.Id
                    && cl.LearningState > LearningState.NotLearned
                    && cl.AppUser == user
                )
            )
            .OrderBy(x => x.JlptLevel)
            .ThenBy(x => x.CharacterType)
            .ThenBy(x => x.Definition);

        return characters;
    }
}