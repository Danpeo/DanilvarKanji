using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Domain.Shared.Entities;
using MediatR;

namespace DanilvarKanji.Application.Reviews.Commands;

public class CreateReviewSessionCommand : IRequest<Result<string>>
{
  public CreateReviewSessionCommand(ICollection<string> exerciseIds, AppUser appUser)
  {
    ExerciseIds = exerciseIds;
    AppUser = appUser;
  }

  public ICollection<string> ExerciseIds { get; set; }

  public AppUser AppUser { get; set; }
}