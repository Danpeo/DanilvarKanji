using System.Reflection;
using AutoMapper;
using DanilvarKanji.Services.Common;
using DanilvarKanji.Domain.DTO;
using DanilvarKanji.Domain.Entities;
using DanilvarKanji.Domain.Enumerations;
using DanilvarKanji.Domain.Params;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DanilvarKanji.Services.Characters;

public class CharacterRepository : Service<ApplicationDbContext>, ICharacterRepository
{
    private readonly IMapper _mapper;

    public CharacterRepository(ApplicationDbContext context, IMapper mapper) : base(context)
    {
        _mapper = mapper;
    }

    public async Task<bool> CreateAsyncObsolete(CharacterDto characterDto)
    {
        TryAction(delegate
        {
            var character = _mapper.Map<Character>(characterDto);

            Context.Characters.Add(character);
        });
        return await SaveAsync();
    }
    
    public void CreateAsync(Character character)
    {
        Context.Characters.Add(character);
    }

    public Task<IEnumerable<Character>> ListAsync(PaginationParams? paginationParams)
    {
        throw new NotImplementedException();
    }

    public Task<Character?> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Character>> SearchAsync(string searchTerm)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Character>> ListChildCharacters(string characterId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(string id, Character character)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<CharacterDto>> ListAsyncObsolete(PaginationParams? paginationParams)
    {
        List<Character> characters = await GetCharactersWithRelatedData(paginationParams)
            .ToListAsync();

        return _mapper.Map<IEnumerable<CharacterDto>>(characters);
    }

    public async Task<CharacterDto> GetAsyncObsolete(string id)
    {
        Character? character = await GetCharactersWithRelatedData()
            .FirstOrDefaultAsync(x => x.Id == id);

        return _mapper.Map<CharacterDto>(character);
    }

    public async Task<IEnumerable<string>> GetKanjiMeaningsByPriority(string characterId, int takeQty,
        Culture culture)
    {
        CharacterDto character = await GetAsyncObsolete(characterId);
        return character.KanjiMeanings
            .Where(x => x.Definitions != null)
            .OrderByDescending(x => x.Priority)
            .Take(takeQty)
            .SelectMany(x => x.Definitions)
            .Where(d => d.Culture == culture)
            .Select(x => x.Value)
            .ToList();
    }

    public async Task<IEnumerable<CharacterDto>> ListChildCharactersObsolete(string id)
    {
        CharacterDto character = await GetAsyncObsolete(id);

        List<Character> childCharacters = new();

        if (character.ChildCharacterIds != null)
            foreach (string childCharacterId in character.ChildCharacterIds)
            {
                Character? child = Context.Characters.FirstOrDefault(x => x.Id == childCharacterId);

                if (child != null)
                    childCharacters.Add(child);
            }

        return _mapper.Map<IEnumerable<CharacterDto>>(childCharacters);
    }

    public async Task<IEnumerable<CharacterDto>> SearchAsyncObsolete(string searchTerm, PaginationParams paginationParams)
    {
        if (string.IsNullOrEmpty(searchTerm) || searchTerm.ToLower() == "any")
            return await ListAsyncObsolete(paginationParams);

        /*List<Character> query = await Context.Characters
            .Where(x => x.Definition != null && x.Definition.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                        x.KanjiMeanings != null && x.KanjiMeanings.Any(kanjiMeaning =>
                            kanjiMeaning.Definitions != null && kanjiMeaning.Definitions.Any(stringDefinition =>
                                stringDefinition.Value.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))) ||
                        x.Kunyomis != null && x.Kunyomis.Any(kunyomi =>
                            kunyomi.Romaji != null &&
                            (kunyomi.JapaneseWriting.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                             kunyomi.Romaji.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))) ||
                        x.Onyomis != null && x.Onyomis.Any(onyomi =>
                            onyomi.Romaji != null &&
                            (onyomi.JapaneseWriting.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                             onyomi.Romaji.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)))).ToListAsync();*/

        List<Character> query = await Context.Characters
            .Where(x =>
                EF.Functions.ILike(x.Definition, $"%{searchTerm}%") ||
                x.KanjiMeanings.Any(km => km.Definitions.Any(d => EF.Functions.ILike(d.Value, $"%{searchTerm}%"))) ||
                x.Kunyomis.Any(k =>
                    EF.Functions.ILike(k.JapaneseWriting, $"%{searchTerm}%") ||
                    EF.Functions.ILike(k.Romaji, $"%{searchTerm}%")) ||
                x.Onyomis.Any(o =>
                    EF.Functions.ILike(o.JapaneseWriting, $"%{searchTerm}%") ||
                    EF.Functions.ILike(o.Romaji, $"%{searchTerm}%")))
            .ToListAsync();

        return _mapper.Map<IEnumerable<CharacterDto>>(query);
    }

    public async Task<CharacterDto?> GetPartialAsync(string id, IEnumerable<string> fields)
    {
        IQueryable<Character> query = GetCharactersWithRelatedData()
            .Where(x => x.Id == id);

        Character? character = await query.FirstOrDefaultAsync();

        CharacterDto? characterDto = _mapper.Map<CharacterDto>(character);

        if (fields.Any())
        {
            PropertyInfo[] propertyInfos = typeof(CharacterDto).GetProperties();
            IEnumerable<PropertyInfo> includedProperties =
                propertyInfos.Where(p => fields.Contains(p.Name, StringComparer.OrdinalIgnoreCase));

            IEnumerable<PropertyInfo> excludedProperties = propertyInfos.Except(includedProperties);

            foreach (PropertyInfo property in excludedProperties)
            {
                property.SetValue(characterDto, null);
            }
        }

        return characterDto;
    }

    public async Task<bool> UpdateAsyncObsolete(string id, CharacterDto characterDto)
    {
        Character? character = Context.Characters.FirstOrDefault(x => x.Id == id);

        if (character != null)
        {
            TryAction(delegate
            {
                _mapper.Map(characterDto, character);
                Context.Characters.Update(character);
            });
        }

        return await SaveAsync();
    }

    public async Task<bool> ReplaceAsync(string id, CharacterDto characterDto)
    {
        Character? existingCharacter = await Context.Characters.FirstOrDefaultAsync(x => x.Id == id);

        if (existingCharacter != null)
        {
            _mapper.Map(characterDto, existingCharacter);

            Context.Entry(existingCharacter).State = EntityState.Modified;
        }

        return await SaveAsync();
    }

    public async Task<bool> DeleteAsync(string id)
    {
        Character? character = Context.Characters.FirstOrDefault(x => x.Id == id);

        if (character != null)
            TryAction(delegate { Context.Characters.Remove(character); });

        return await SaveAsync();
    }

    public Task<bool> Exist(string id) =>
        Context.Characters.AnyAsync(x => x.Id == id);

    public Task<bool> AnyExist() =>
        Context.Characters.AnyAsync();

    private IQueryable<Character> GetCharactersWithRelatedData(PaginationParams? paginationParams = null)
    {
        var characters = Context.Characters
            .Include(x => x.Mnemonics)
            .Include(x => x.KanjiMeanings)
            .ThenInclude(x => x.Definitions)
            .Include(x => x.Kunyomis)
            .Include(x => x.Onyomis)
            .Include(x => x.Words)
            .ThenInclude(x => x.WordMeanings);

        if (paginationParams != null && paginationParams.PageNumber != 0 && paginationParams.PageSize != 0)
        {
            return characters
                .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                .Take(paginationParams.PageSize);
        }

        return characters;
    }
}