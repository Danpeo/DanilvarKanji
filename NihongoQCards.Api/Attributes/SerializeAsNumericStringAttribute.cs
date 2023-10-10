using System.Text.Json.Serialization;
using DanilvarKanji.Utils;

namespace DanilvarKanji.Attributes;

public class SerializeAsNumericStringAttribute : JsonConverterAttribute
{
    public SerializeAsNumericStringAttribute()
        : base(typeof(NumberToStringConverter))
    {
    }
}