using DanilvarKanji.Domain.Entities;
using DanilvarKanji.Domain.Primitives.Result;
using MediatR;

namespace DanilvarKanji.Application.Reviews.Commands;

public class CreateReviewSessionCommand : IRequest<Result<string>>
{
    public ICollection<string> ExerciseIds { get; set; }

    public AppUser AppUser { get; set; }

    public CreateReviewSessionCommand(ICollection<string> exerciseIds, AppUser appUser)
    {
        ExerciseIds = exerciseIds;
        AppUser = appUser;
    }
}