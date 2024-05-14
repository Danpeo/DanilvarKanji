using System.ComponentModel;
using System.Reflection;

namespace DanilvarKanji.Infrastructure.Common;

public static class EnumHelper
{
    public static BindingList<string?> GetEnumDescriptions(Type enumType)
    {
        Array values = Enum.GetValues(enumType);
        var descriptions = new BindingList<string?>();

        foreach (var value in values)
        {
            FieldInfo field = enumType.GetField(value.ToString());
            var descriptionAttribute = (DescriptionAttribute)
                Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));

            descriptions.Add(
                descriptionAttribute != null ? descriptionAttribute.Description : value.ToString()
            );
        }

        return descriptions;
    }

    public static string? GetEnumDescription(Enum enumeration)
    {
        FieldInfo? fieldInfo = enumeration.GetType().GetField(enumeration.ToString());

        if (fieldInfo != null)
        {
            if (
                Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute))
                is DescriptionAttribute descriptionAttribute
            )
                return descriptionAttribute.Description;

            return enumeration.ToString();
        }

        return default;
    }
}