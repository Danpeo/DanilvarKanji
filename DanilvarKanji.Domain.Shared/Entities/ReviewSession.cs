using Danilvar.Entity;

namespace DanilvarKanji.Domain.Shared.Entities;

public class ReviewSession : Entity
{
    public ICollection<Exercise> Exercises { get; set; }

    public AppUser AppUser { get; set; }

    public DateTime ReviewDataTime { get; set; } = DateTime.UtcNow;

    public ReviewSession()
    {
    }
    
    public ReviewSession(ICollection<Exercise> exercises, AppUser appUser) : this()
    {
        Exercises = exercises;
        AppUser = appUser;
    }
}