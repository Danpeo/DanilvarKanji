using System.Linq.Expressions;
using AutoMapper;
using DanilvarKanji.Data;
using DanilvarKanji.DTO;
using DanilvarKanji.Models;
using DanilvarKanji.Services.Common;
using Microsoft.EntityFrameworkCore;

namespace DanilvarKanji.Services.Characters;

public class CharacterLearningManagementService : Service<ApplicationDbContext>, ICharacterLearningManagementService
{
    private readonly IMapper _mapper;

    public CharacterLearningManagementService(ApplicationDbContext context, IMapper mapper) :
        base(context)
    {
        _mapper = mapper;
    }

    public async Task<bool> CreateAsync(CharacterLearningDto characterDto, AppUser appUser)
    {
        await TryActionAsync(async () =>
        {
            var characterLearning = new CharacterLearning()
            {
                AppUser = appUser,
                Character = await Context.Characters
                    .FirstOrDefaultAsync(x => x.Id == characterDto.CharacterId) ?? new Character(),
                LearningProgress = 0.0f,
                LearningState = characterDto.LearningState
            };

            Context.CharacterLearnings.Add(characterLearning);
        });

        return await SaveAsync();
    }

    public async Task<bool> Update(string id, CharacterLearningDto characterDto)
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

    public async Task<bool> DeleteAsync(string id) =>
        await DeleteWithFilterAsync(x => x.Id == id);

    public async Task<bool> DeleteForUserAsync(string id, AppUser appUser) =>
        await DeleteWithFilterAsync(x => x.Id == id && x.AppUser == appUser);

    public async Task<IEnumerable<CharacterLearningDto>> ListAsync() =>
        await GetAllCharacterLearningsAsync();

    public async Task<IEnumerable<CharacterLearningDto>> ListForUserAsync(AppUser? appUser) =>
        await GetAllCharacterLearningsAsync(appUser);

    public async Task<CharacterLearningDto> GetAsync(string id) => 
        await GetWithFilterAsync(x => x.Id == id);

    public async Task<CharacterLearningDto> GetForUserAsync(string id, AppUser? appUser) => 
        await GetWithFilterAsync(x => x.Id == id && x.AppUser == appUser);

    public Task<bool> Exist(string id) =>
        Context.CharacterLearnings.AnyAsync(x => x.Id == id);

    private async Task<bool> DeleteWithFilterAsync(Expression<Func<CharacterLearning, bool>> filter)
    {
        CharacterLearning? characterLearning = await Context.CharacterLearnings.FirstOrDefaultAsync(filter);

        if (characterLearning != null)
            TryAction(delegate { Context.CharacterLearnings.Remove(characterLearning); });

        return await SaveAsync();
    }

    private async Task<IEnumerable<CharacterLearningDto>> GetAllCharacterLearningsAsync(AppUser? appUser = null)
    {
        IQueryable<CharacterLearning> query = Context.CharacterLearnings;

        if (appUser != null)
            query = query.Where(x => x.AppUser == appUser);

        List<CharacterLearning> characterLearnings = await query.ToListAsync();

        return _mapper.Map<IEnumerable<CharacterLearningDto>>(characterLearnings);
    }

    private async Task<CharacterLearningDto> GetWithFilterAsync(Expression<Func<CharacterLearning, bool>> filter)
    {
        CharacterLearning? characterLearning = await Context.CharacterLearnings
            .FirstOrDefaultAsync(filter);

        return _mapper.Map<CharacterLearningDto>(characterLearning);
    }
}