using System.ComponentModel.DataAnnotations;
using System.Reflection;
using DanilvarKanji.Domain.Shared.Enumerations;
using DanilvarKanji.Shared.Responses.Exercise;

namespace DanilvarKanji.Client.Extensions;

public static class EnumExtension
{
  public static string? GetDisplayName(this Enum value)
  {
    FieldInfo? fieldInfo = value.GetType().GetField(value.ToString());
    if (fieldInfo != null)
    {
      var attribute = fieldInfo.GetCustomAttribute<DisplayAttribute>();
      if (attribute != null) return attribute.Name;
    }

    return value.ToString();
  }

  public static string FromExerciseTypeToText(this ExerciseResponseBase e)
  {
    return e.ExerciseSubject switch
    {
      ExerciseSubject.Meaning => "Meaning",
      ExerciseSubject.Kunyomi => "Kunyomi",
      ExerciseSubject.Onyomi => "Onyomi",
      _ => "None"
    };
  }
}