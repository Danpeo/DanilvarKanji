namespace DanilvarKanji.Domain.Entities;

public class TEST
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public static bool operator ==(TEST left, TEST right)
    {
        return left.Name.Equals(right.Name);
    }

    public static bool operator !=(TEST left, TEST right)
    {
        return !(left == right);
    }
}