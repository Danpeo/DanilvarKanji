using DanilvarKanji.Data;
using DanilvarKanji.DTO;
using DanilvarKanji.Models;
using DanilvarKanji.Services.Common;

namespace DanilvarKanji.Services;

public interface ICharacterService
{
    Task<bool> CreateAsync(CharacterDto characterDto);
}

public class CharacterService : Service<ApplicationDbContext>, ICharacterService
{
    public CharacterService(ApplicationDbContext context) : base(context)
    {
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
}