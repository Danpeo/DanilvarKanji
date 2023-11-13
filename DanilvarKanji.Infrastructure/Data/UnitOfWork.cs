using DanilvarKanji.Infrastructure.Abstractions;

namespace DanilvarKanji.Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<bool> CompleteAsync() => 
        await _context.SaveChangesAsync() > 0;

    public bool HasChanges() => 
        _context.ChangeTracker.HasChanges();
}