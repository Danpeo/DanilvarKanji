using DanilvarKanji.Domain.Shared.Enumerations;

namespace DanilvarKanji.Domain.Shared.Params;

public class LearningSettings
{
  public LearningSettings()
  {
  }

  public LearningSettings(int qtyOfCharsForLearningForDay, JlptLevel jlptLevel)
  {
    QtyOfCharsForLearningForDay = qtyOfCharsForLearningForDay;
    JlptLevel = jlptLevel;
  }

  public JlptLevel JlptLevel { get; set; }

  public int QtyOfCharsForLearningForDay { get; set; }
}