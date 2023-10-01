using DanilvarKanji.Data;
using DanilvarKanji.DTO;
using DanilvarKanji.Models;
using DanilvarKanji.Services.Common;

namespace DanilvarKanji.Services.Characters;

public class CharacterLearningService : Service<ApplicationDbContext>, ICharacterLearningService
{
    public CharacterLearningService(ApplicationDbContext context) :
        base(context)
    {
    }

    public async Task<bool> UpdateCharacterLearningState(CharacterForLearnDto characterDto, AppUser appUser)
    {
        TryAction(delegate
        {
            var characterLearning = new CharacterLearning()
            {
                AppUser = appUser,
                Character = Context.Characters.FirstOrDefault(x => x.Id == characterDto.Id) ?? new Character(),
                LearningProgress = 0.0f,
                LearningState = characterDto.LearningState
            };
            
            Context.CharacterLearnings.Add(characterLearning);
        });

        return await SaveAsync();
    }
}