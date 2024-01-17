namespace DanilvarKanji.Shared.Requests.Reviews;

public class CreateReviewSessionRequest
{
    public ICollection<string> ExerciseIds { get; set; }


    public CreateReviewSessionRequest()
    {
        ExerciseIds = new List<string>();
    }

    public CreateReviewSessionRequest(ICollection<string> exerciseIds)
    {
        ExerciseIds = exerciseIds;
    }
}