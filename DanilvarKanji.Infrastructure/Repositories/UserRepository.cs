using DanilvarKanji.Domain.Entities;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Domain.Shared.Entities;
using DanilvarKanji.Domain.Shared.Enumerations;
using DanilvarKanji.Domain.Shared.Params;
using DanilvarKanji.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DanilvarKanji.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void CreateEmailCode(EmailCode emailCode) => _context.EmailCodes.Add(emailCode);

    public async Task<string?> GetRegistrationConfirmationCodeAsync(string email)
    {
        EmailCode? emailCode = await GetEmailCodeAsync(email);
        return emailCode?.GeneratedCode;
    }

    public async Task DeleteEmailCodeAsync(string email)
    {
        _context.EmailCodes.Remove((await GetEmailCodeAsync(email))!);
    }

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
        return await _context.AppUsers.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<LearningSettings?> GetUserLearningSettingsAsync(string email)
    {
        AppUser? user = await _context.AppUsers.FirstOrDefaultAsync(u => u!.Email == email);
        if (user != null)
            return new LearningSettings
            {
                JlptLevel = user.JlptLevel,
                QtyOfCharsForLearningForDay = user.QtyOfCharsForLearningForDay
            };

        return new LearningSettings();
    }

    public async ValueTask<bool> AnyExistAsync() => await _context.Characters.AnyAsync();

    public async Task<AppUser?> GetByEmailAsync(string email) =>
        await _context.AppUsers.FirstOrDefaultAsync(x => x.Email == email);

    public IEnumerable<AppUser> List(PaginationParams paginationParams) =>
        _context.AppUsers.Where(u => u.Role != UserRole.SuperAdmin).AsEnumerable().Paginate(paginationParams);

    public async Task<bool> ExistById(string id) => await _context.AppUsers.AnyAsync(user => user.Id == id);

    public Task<bool> AnyExist() => _context.AppUsers.AnyAsync();

    public async Task<bool> ExistByEmail(string email) => await _context.AppUsers.AnyAsync(user => user.Email == email);

    private async Task<EmailCode?> GetEmailCodeAsync(string email)
    {
        EmailCode? emailCode = await _context.EmailCodes.FirstOrDefaultAsync(e => e.Email == email);
        return emailCode;
    }

    private async Task<AppUser?> GetUserByEmail(string email) =>
        await _context.AppUsers.FirstOrDefaultAsync(u => u.Email == email);
}