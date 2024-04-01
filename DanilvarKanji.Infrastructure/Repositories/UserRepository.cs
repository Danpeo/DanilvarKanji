using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Infrastructure.Common;
using DanilvarKanji.Infrastructure.Data;
using DanilvarKanji.Shared.Domain.Entities;
using DanilvarKanji.Shared.Domain.Params;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace DanilvarKanji.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public void Create(AppUser? user) =>
        _context.AppUsers.Add(user);

    public async Task UpdateUserLearningSettingsAsync(string email, LearningSettings learningSettings)
    {
        AppUser? user = await GetUserByEmail(email);
        if (user != null)
        {
            user.JlptLevel = learningSettings.JlptLevel;
            user.QtyOfCharsForLearningForDay = learningSettings.QtyOfCharsForLearningForDay;
        }
    }

    public async Task UpdateUserAsync(string email, string newUserName, string newUserRole)
    {
        AppUser? user = await GetUserByEmail(email);

        if (user is not null)
        {
            user.UserName = newUserName;
            user.Role = newUserRole;
            _context.AppUsers.Update(user);
        }
    }

    public async Task DeleteAsync(string email)
    {
        AppUser? appUser = await GetByEmailAsync(email);
        _context.AppUsers.Remove(appUser!);
    }

    public async Task<AppUser?> GetByIdAsync(string id)
    {
        return await _context.AppUsers
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<LearningSettings?> GetUserLearningSettingsAsync(string email)
    {
        AppUser? user = await _context.AppUsers.FirstOrDefaultAsync(u => u!.Email == email);
        if (user != null)
        {
            return new LearningSettings
            {
                JlptLevel = user.JlptLevel,
                QtyOfCharsForLearningForDay = user.QtyOfCharsForLearningForDay
            };
        }

        return new LearningSettings();
    }

    public async Task UpdateUserXpAsync(int xp, string email)
    {
        AppUser? user = await _context.AppUsers.FirstOrDefaultAsync(u => u!.Email == email);
        if (user != null) user.XP = xp;
    }

    public async ValueTask<bool> AnyExistAsync() =>
        await _context.Characters.AnyAsync();

    public async Task<AppUser?> GetByEmailAsync(string email)
    {
        return await _context.AppUsers
            .FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<IEnumerable<AppUser>> ListAsync(PaginationParams? paginationParams)
    {
        List<AppUser> users = await _context.AppUsers
            .ToListAsync();

        return paginationParams != null ? Paginator.Paginate(users, paginationParams) : users;
    }

    public async Task<bool> ExistById(string id) =>
        await _context.AppUsers.AnyAsync(x => x.Id == id);

    public Task<bool> AnyExist() =>
        _context.AppUsers.AnyAsync();

    public async Task<bool> ExistByEmail(string email) =>
        await _context.AppUsers.AnyAsync(x => x!.Email == email);

    private async Task<AppUser?> GetUserByEmail(string email) =>
        await _context.AppUsers.FirstOrDefaultAsync(u => u.Email == email);
}