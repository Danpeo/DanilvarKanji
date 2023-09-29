using Microsoft.EntityFrameworkCore;

namespace DanilvarKanji.Services.Common;

public abstract class Service<TDbContext> where TDbContext : DbContext
{
    protected readonly TDbContext Context;
    
    protected Service(TDbContext context)
    {
        Context = context;
    }

    protected async void TryAction(Action action)
    {
        try
        {
            action.Invoke();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    protected virtual async Task<bool> SaveAsync()
    {
        int saved = await Context.SaveChangesAsync();
        return saved > 0;
    }
}