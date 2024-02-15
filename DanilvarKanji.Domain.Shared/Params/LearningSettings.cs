using DanilvarKanji.Shared.Domain.Enumerations;

namespace DanilvarKanji.Shared.Domain.Params;

public class LearningSettings
{
    public JlptLevel JlptLevel { get; set; }

    public int QtyOfCharsForLearningForDay { get; set; }
    
    public LearningSettings()
    {
        
    }

    public LearningSettings(int qtyOfCharsForLearningForDay, JlptLevel jlptLevel)
    {
        QtyOfCharsForLearningForDay = qtyOfCharsForLearningForDay;
        JlptLevel = jlptLevel;
    }
}