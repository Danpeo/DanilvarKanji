using AutoMapper;

namespace DanilvarKanji.Utils;

public class EnumToStringConverter : IValueConverter<Enum, string>
{
    public string Convert(Enum sourceMember, ResolutionContext context)
    {
        return sourceMember.ToString();
    }
}