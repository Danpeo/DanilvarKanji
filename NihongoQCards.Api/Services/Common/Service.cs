using Microsoft.EntityFrameworkCore;

namespace DanilvarKanji.Services.Common;

public abstract class Service<TDbContext> where TDbContext : DbContext
{
    protected readonly TDbContext Context;
    
    protected Service(TDbContext context)
    {
        Context = context;
    }

    protected void TryAction(Action action)
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
    
    protected async Task TryActionAsync(Func<Task> action)
    {
        try
        {
            await action.Invoke();
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