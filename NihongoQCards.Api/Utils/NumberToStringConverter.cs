using System.Text.Json;
using System.Text.Json.Serialization;

namespace DanilvarKanji.Utils;

public class NumberToStringConverter : JsonConverter<string>
{
    public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            return reader.GetString().Substring(2,3);
        }

        if (reader.TokenType == JsonTokenType.Number)
        {
            return reader.GetDecimal().ToString(); // Пример преобразования числа в строку
        }
        else
        {
            throw new JsonException($"Unexpected token type: {reader.TokenType}");
        }
    }

    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value);
    }
}