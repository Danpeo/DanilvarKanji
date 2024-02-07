using DanilvarKanji.Domain.Events;
using DanilvarKanji.Shared.Domain.Entities;

namespace DanilvarKanji.Application.Reviews.Events;

public class CompletedReviewDomainEvent : IDomainEvent
{
    public ICollection<Exercise> Exercises { get; init; }

    public AppUser AppUser { get; init; }

    public DateTime ReviewDataTime { get; init; }

    public CompletedReviewDomainEvent(ICollection<Exercise> exercises, AppUser appUser, DateTime reviewDataTime)
    {
        Exercises = exercises;
        AppUser = appUser;
        ReviewDataTime = reviewDataTime;
    }
}

