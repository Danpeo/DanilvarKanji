using System.ComponentModel.DataAnnotations;
using System.Reflection;
using DanilvarKanji.Domain.Enumerations;
using DanilvarKanji.Shared.Responses.Exercise;

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
    
    public static string FromExerciseTypeToText(this GetBaseExerciseInfoResponse e)
    {
        return e.ExerciseType switch
        {
            ExerciseType.Meaning => "Meaning",
            ExerciseType.Kunyomi => "Kunyomi",
            ExerciseType.Onyomi => "Onyomi",
            _ => "None"
        };
    }
}