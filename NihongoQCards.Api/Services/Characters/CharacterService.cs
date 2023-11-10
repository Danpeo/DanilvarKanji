using System.Reflection;
using AutoMapper;
using DanilvarKanji.Data;
using DanilvarKanji.Services.Common;
using DanilvarKanji.Shared.DTO;
using DanilvarKanji.Shared.Entities;
using DanilvarKanji.Shared.Entities.Common;
using DanilvarKanji.Shared.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace DanilvarKanji.Services.Characters;

public class CharacterService : Service<ApplicationDbContext>, ICharacterService
{
    private readonly IMapper _mapper;

    public CharacterService(ApplicationDbContext context, IMapper mapper) : base(context)
    {
        _mapper = mapper;
    }

    public async Task<bool> CreateAsync(CharacterDto characterDto)
    {
        TryAction(delegate
        {
            var character = _mapper.Map<Character>(characterDto);

            Context.Characters.Add(character);
        });
        return await SaveAsync();
    }

    public async Task<IEnumerable<CharacterDto>> ListAsync(PaginationParams? paginationParams)
    {
        List<Character> characters = await GetCharactersWithRelatedData(paginationParams)
            .ToListAsync();

        return _mapper.Map<IEnumerable<CharacterDto>>(characters);
    }

    public async Task<CharacterDto> GetAsync(string id)
    {
        Character? character = await GetCharactersWithRelatedData()
            .FirstOrDefaultAsync(x => x.Id == id);

        return _mapper.Map<CharacterDto>(character);
    }

    public async Task<IEnumerable<string>> GetKanjiMeaningsByPriority(string characterId, int takeQty,
        Culture culture)
    {
        CharacterDto character = await GetAsync(characterId);
        return character.KanjiMeanings
            .Where(x => x.Definitions != null)
            .OrderByDescending(x => x.Priority)
            .Take(takeQty)
            .SelectMany(x => x.Definitions)
            .Where(d => d.Culture == culture)
            .Select(x => x.Value)
            .ToList();
    }

    public async Task<IEnumerable<CharacterDto>> ListChildCharacters(string id)
    {
        CharacterDto character = await GetAsync(id);

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

    public async Task<IEnumerable<CharacterDto>> SearchAsync(string searchTerm, PaginationParams paginationParams)
    {
        if (string.IsNullOrEmpty(searchTerm) || searchTerm.ToLower() == "any")
            return await ListAsync(paginationParams);

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

    public async Task<bool> UpdateAsync(string id, CharacterDto characterDto)
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