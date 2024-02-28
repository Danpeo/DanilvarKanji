namespace DanilvarKanji.Client.State;

public abstract class StateBase
{
    protected bool IsInitialized;
    public abstract Task Init();
}