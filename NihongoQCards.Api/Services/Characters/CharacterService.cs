using System.Reflection;
using AutoMapper;
using DanilvarKanji.Data;
using DanilvarKanji.Services.Common;
using DanilvarKanji.Shared.DTO;
using DanilvarKanji.Shared.Models;
using Microsoft.EntityFrameworkCore;

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

    public async Task<IEnumerable<CharacterDto>> ListAsync()
    {
        List<Character> characters = await GetCharactersWithRelatedData()
            .ToListAsync();

        return _mapper.Map<IEnumerable<CharacterDto>>(characters);
    }

    public async Task<CharacterDto> GetAsync(string id)
    {
        Character? character = await GetCharactersWithRelatedData()
            .FirstOrDefaultAsync(x => x.Id == id);

        return _mapper.Map<CharacterDto>(character);
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

    private IQueryable<Character> GetCharactersWithRelatedData()
    {
        return Context.Characters
            .Include(x => x.KanjiMeanings)
            .Include(x => x.Kunyomis)
            .Include(x => x.Onyomis)
            .Include(x => x.Words)
            .ThenInclude(x => x.WordMeanings);
    }
}