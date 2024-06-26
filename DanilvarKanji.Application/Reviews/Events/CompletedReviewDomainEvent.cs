using DanilvarKanji.Domain.Events;
using DanilvarKanji.Domain.Shared.Entities;

namespace DanilvarKanji.Application.Reviews.Events;

public class CompletedReviewDomainEvent : IDomainEvent
{
    public CompletedReviewDomainEvent(
        ICollection<Exercise> exercises,
        AppUser appUser,
        DateTime reviewDataTime
    )
    {
        Exercises = exercises;
        AppUser = appUser;
        ReviewDataTime = reviewDataTime;
    }

    public ICollection<Exercise> Exercises { get; init; }

    public AppUser AppUser { get; init; }

    public DateTime ReviewDataTime { get; init; }
}