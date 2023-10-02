using System.Linq.Expressions;
using AutoMapper;
using DanilvarKanji.Data;
using DanilvarKanji.DTO;
using DanilvarKanji.Models;
using DanilvarKanji.Services.Common;
using Microsoft.EntityFrameworkCore;

namespace DanilvarKanji.Services.Characters;

public class CharacterLearningService : Service<ApplicationDbContext>, ICharacterLearningService
{
    private readonly IMapper _mapper;

    public CharacterLearningService(ApplicationDbContext context, IMapper mapper) :
        base(context)
    {
        _mapper = mapper;
    }

    public async Task<bool> AddCharacterLearning(CharacterForLearnDto characterDto, AppUser appUser)
    {
        await TryActionAsync(async () =>
        {
            var characterLearning = new CharacterLearning()
            {
                AppUser = appUser,
                Character = await Context.Characters
                    .FirstOrDefaultAsync(x => x.Id == characterDto.Id) ?? new Character(),
                LearningProgress = 0.0f,
                LearningState = characterDto.LearningState
            };

            Context.CharacterLearnings.Add(characterLearning);
        });

        return await SaveAsync();
    }

    public async Task<bool> UpdateCharacterLearning(int id, CharacterForLearnDto characterDto)
    {
        await TryActionAsync(async () =>
        {
            CharacterLearning? characterLearning = await Context.CharacterLearnings
                .FirstOrDefaultAsync(x => x.Id == id);

            if (characterLearning != null)
            {
                _mapper.Map(characterDto, characterLearning);
                Context.CharacterLearnings.Update(characterLearning);
            }
        });

        return await SaveAsync();
    }

    public async Task<bool> DeleteAsync(int id) =>
        await DeleteWithFilterAsync(x => x.Id == id);

    public async Task<bool> DeleteForUserAsync(int id, AppUser appUser) =>
        await DeleteWithFilterAsync(x => x.Id == id && x.AppUser == appUser);

    public async Task<IEnumerable<CharacterForLearnDto>> GetAllAsync() =>
        await GetAllCharacterLearningsAsync();

    public async Task<IEnumerable<CharacterForLearnDto>> GetAllForUserAsync(AppUser? appUser) =>
        await GetAllCharacterLearningsAsync(appUser);

    public async Task<CharacterForLearnDto> GetAsync(int id) => 
        await GetWithFilterAsync(x => x.Id == id);

    public async Task<CharacterForLearnDto> GetForUserAsync(int id, AppUser? appUser) => 
        await GetWithFilterAsync(x => x.Id == id && x.AppUser == appUser);

    public Task<bool> Exist(int id) =>
        Context.CharacterLearnings.AnyAsync(x => x.Id == id);

    private async Task<bool> DeleteWithFilterAsync(Expression<Func<CharacterLearning, bool>> filter)
    {
        CharacterLearning? characterLearning = await Context.CharacterLearnings.FirstOrDefaultAsync(filter);

        if (characterLearning != null)
            TryAction(delegate { Context.CharacterLearnings.Remove(characterLearning); });

        return await SaveAsync();
    }

    private async Task<IEnumerable<CharacterForLearnDto>> GetAllCharacterLearningsAsync(AppUser? appUser = null)
    {
        IQueryable<CharacterLearning> query = Context.CharacterLearnings;

        if (appUser != null)
            query = query.Where(x => x.AppUser == appUser);

        List<CharacterLearning> characterLearnings = await query.ToListAsync();

        return _mapper.Map<IEnumerable<CharacterForLearnDto>>(characterLearnings);
    }

    private async Task<CharacterForLearnDto> GetWithFilterAsync(Expression<Func<CharacterLearning, bool>> filter)
    {
        CharacterLearning? characterLearning = await Context.CharacterLearnings
            .FirstOrDefaultAsync(filter);

        return _mapper.Map<CharacterForLearnDto>(characterLearning);
    }
}