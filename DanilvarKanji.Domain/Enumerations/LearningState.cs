namespace DanilvarKanji.Domain.Enumerations;

public enum LearningState
{
    //[EnumMember(Value = nameof(NotLearned))]
    NotLearned,
    
   // [EnumMember(Value = nameof(Learning))]
    Learning,
    
    //[EnumMember(Value = nameof(LearnedForSomeTime))]
    LearnedForSomeTime,
    
    //[EnumMember(Value = nameof(Skipped))]
    Skipped,
    
    //[EnumMember(Value = nameof(CompletelyLearned))]
    CompletelyLearned
}