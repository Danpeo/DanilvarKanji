namespace DanilvarKanji.Infrastructure.Common;

public class MachineDateTime : IDateTime
{
  public DateTime UtcNow => DateTime.UtcNow;
}