namespace DanilvarKanji.Shared.Requests.Reviews;

public class CreateReviewSessionRequest
{
  public CreateReviewSessionRequest()
  {
    ExerciseIds = new List<string>();
  }

  public CreateReviewSessionRequest(ICollection<string> exerciseIds)
  {
    ExerciseIds = exerciseIds;
  }

  public ICollection<string> ExerciseIds { get; set; }
}