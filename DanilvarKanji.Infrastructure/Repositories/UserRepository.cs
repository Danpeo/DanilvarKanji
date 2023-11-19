using DanilvarKanji.Domain.Entities;
using DanilvarKanji.Domain.Params;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DanilvarKanji.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public void Create(AppUser user) => 
        _context.AppUsers.Add(user);

    public Task<AppUser> GetByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<AppUser> GetByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<AppUser>> ListAsync(PaginationParams paginationParams)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ExistById(string id) => 
        await _context.AppUsers.AnyAsync(x => x.Id == id);

    public Task<bool> AnyExist() => 
        _context.AppUsers.AnyAsync();

    public async Task<bool> ExistByEmail(string email) => 
        await _context.AppUsers.AnyAsync(x => x.Email == email);
}