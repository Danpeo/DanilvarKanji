using DanilvarKanji.Data;
using DanilvarKanji.Services.Common;

namespace DanilvarKanji.Services.Infrastructure;

public class UnitOfWork : Service<ApplicationDbContext>, IAsyncDisposable, IUnitOfWork
{
    public UnitOfWork(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<bool> CompleteAsync() => 
        await Context.SaveChangesAsync() > 0;

    public bool HasChanges() => 
        Context.ChangeTracker.HasChanges();

    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        await Context.DisposeAsync();
    }
}
