using System.Runtime.Serialization;

namespace DanilvarKanji.Models.Enums;

public enum CharacterType
{
    [EnumMember(Value = nameof(Radical))]
    Radical,
    
    [EnumMember(Value = nameof(Kanji))]
    Kanji
}