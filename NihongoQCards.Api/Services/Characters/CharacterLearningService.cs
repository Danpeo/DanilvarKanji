using DanilvarKanji.Data;
using DanilvarKanji.Services.Common;

namespace DanilvarKanji.Services.Characters;

public class CharacterLearningService : Service<ApplicationDbContext>
{
    public CharacterLearningService(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<bool> AddCharacterForLearning()
    {
        return true;
    }
}