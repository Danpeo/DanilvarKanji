using DanilvarKanji.Shared.Requests.Exercises;
using DanilvarKanji.Shared.Responses.Exercise;

namespace DanilvarKanji.Client.Services.Review;

public interface IExerciseService
{
    Task<GetAllFromExerciseResponse?> CreateExerciseAsync(CreateExerciseRequest request);
}