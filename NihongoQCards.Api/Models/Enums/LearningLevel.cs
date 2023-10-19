using System.Runtime.Serialization;

namespace DanilvarKanji.Models.Enums;

public enum LearningLevel
{
    [EnumMember(Value = nameof(Novice))]
    Novice,

    [EnumMember(Value = nameof(Scholar))]
    Scholar,
    
    [EnumMember(Value = nameof(Virtuoso))]
    Virtuoso,

    [EnumMember(Value = nameof(Luminary))]
    Luminary,
    
    [EnumMember(Value = nameof(Maestro))]
    Maestro
}