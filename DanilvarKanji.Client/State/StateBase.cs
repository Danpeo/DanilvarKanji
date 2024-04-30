namespace DanilvarKanji.Client.State;

public abstract class StateBase<TState>
{
    protected bool IsInitialized;
    public abstract Task InitAsync();

    public virtual async Task UpdateAsync(TState newState)
    {
        
    }
}