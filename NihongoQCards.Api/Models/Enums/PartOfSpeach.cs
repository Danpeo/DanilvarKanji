using System.Runtime.Serialization;

namespace DanilvarKanji.Models.Enums;

public enum PartOfSpeach
{
    [EnumMember(Value = nameof(Noun))]
    Noun,
    
    [EnumMember(Value = nameof(Pronoun))]
    Pronoun,
    
    [EnumMember(Value = nameof(Verb))]
    Verb,
    
    [EnumMember(Value = nameof(Adjective))]
    Adjective,
    
    [EnumMember(Value = nameof(Adverb))]
    Adverb,
    
    [EnumMember(Value = nameof(Conjunction))]
    Conjunction,
    
    [EnumMember(Value = nameof(Particle))]
    Particle,
    
    [EnumMember(Value = nameof(Interjection))]
    Interjection,
    
    [EnumMember(Value = nameof(Determiner))]
    Determiner,
    
    [EnumMember(Value = nameof(Counter))]
    Counter,
    
    [EnumMember(Value = nameof(Prefix))]
    Prefix,
    
    [EnumMember(Value = nameof(Suffix))]
    Suffix 
}