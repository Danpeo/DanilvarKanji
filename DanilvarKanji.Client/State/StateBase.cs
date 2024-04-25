namespace DanilvarKanji.Client.State;

public abstract class StateBase<TState>
{
    protected bool IsInitialized;
    public abstract Task Init();

    public virtual async Task UpdateAsync(TState newState)
    {
        
    }
}