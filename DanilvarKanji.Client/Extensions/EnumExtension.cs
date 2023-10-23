using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace DanilvarKanji.Client.Extensions;

public static class EnumExtension
{
    public static string? GetDisplayName(this Enum value)
    {
        FieldInfo? fieldInfo = value.GetType().GetField(value.ToString());
        if (fieldInfo != null)
        {
            DisplayAttribute? attribute = fieldInfo.GetCustomAttribute<DisplayAttribute>();
            if (attribute != null)
            {
                return attribute.Name;
            }
        }

        return value.ToString();
    }
}