using AutoMapper;
using DanilvarKanji.Data;
using DanilvarKanji.DTO;
using DanilvarKanji.Models;
using DanilvarKanji.Services.Common;
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
            var character = new Character(characterDto);
            Context.Characters.Add(character);
        });
        return await SaveAsync();
    }

    public async Task<IEnumerable<CharacterDto>> GetAllAsync()
    {
        var characters = await Context.Characters
            .Include(x => x.KanjiMeanings)
            .Include(x => x.Kunyomis)
            .Include(x => x.Onyomis)
            .Include(x => x.SampleWords)
            .ToListAsync();

        return _mapper.Map<IEnumerable<CharacterDto>>(characters);
    }

    public async Task<CharacterDto> GetAsync(int id)
    {
        Character? character = await Context.Characters
            .Include(x => x.KanjiMeanings)
            .Include(x => x.Kunyomis)
            .Include(x => x.Onyomis)
            .Include(x => x.SampleWords)
            .FirstOrDefaultAsync(x => x.Id == id);

        return _mapper.Map<CharacterDto>(character);
    }

    public async Task<bool> UpdateAsync(int id, CharacterDto characterDto)
    {
        Character? character = Context.Characters.FirstOrDefault(x => x.Id == id);

        if (character == null)
        {
            TryAction(delegate { _mapper.Map(characterDto, character); });
        }

        return await SaveAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        Character? character = Context.Characters.FirstOrDefault(x => x.Id == id);

        if (character != null)
        {
            TryAction(delegate { Context.Characters.Remove(character); });
        }

        return await SaveAsync();
    }

    public bool Exist(int id) =>
        Context.Characters.Any(x => x.Id == id);
}