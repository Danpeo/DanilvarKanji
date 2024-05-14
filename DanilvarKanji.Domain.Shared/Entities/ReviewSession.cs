using Danilvar.Entity;

namespace DanilvarKanji.Domain.Shared.Entities;

public class ReviewSession : Entity
{
    public ReviewSession()
    {
    }

    public ReviewSession(ICollection<Exercise> exercises, AppUser appUser)
        : this()
    {
        Exercises = exercises;
        AppUser = appUser;
    }

    public ICollection<Exercise> Exercises { get; set; }

    public AppUser AppUser { get; set; }

    public DateTime ReviewDataTime { get; set; } = DateTime.UtcNow;
}