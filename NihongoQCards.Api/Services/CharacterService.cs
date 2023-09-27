using NihongoQCards.Data;
using NihongoQCards.DTO;
using NihongoQCards.Models;
using NihongoQCards.Services.Common;

namespace NihongoQCards.Services;

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