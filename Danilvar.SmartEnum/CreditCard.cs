namespace Danilvar.SmartEnum;

public class CreditCard : Enumeration<CreditCard>
{
    public static readonly CreditCard Standart = new(1, "Standart");
    public static readonly CreditCard Premium = new(1, "Premium");
    public static readonly CreditCard Platinum = new(1, "Platinum");

    public double Discount => 0.0;
    
    private CreditCard(int value, string name) : base(value, name)
    {
    }
}