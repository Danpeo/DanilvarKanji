namespace DVar.Extensions;

public static class Lazy
{
    public static void DoubleFor(int upperI, int upperJ, Action action)
    {
        for (var i = 0; i < upperI; i++)
        for (var j = 0; j < upperJ; j++)
            action.Invoke();
    }
}