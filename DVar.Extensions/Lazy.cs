namespace DVar.Extensions;

public static class Lazy
{
    public static void DoubleFor(int upperI, int upperJ, Action action)
    {
        for (int i = 0; i < upperI; i++)
        for (int j = 0; j < upperJ; j++)
            action.Invoke();
    }
}