using Danilvar.Entity;

namespace DanilvarKanji.Domain.Shared.Entities.Flashcards;

public class FlashcardCollection : Entity
{
    public FlashcardCollection()
    {
    }

    public FlashcardCollection(string name, List<Flashcard> flashcards, AppUser appUser)
    {
        Name = name;
        Flashcards = flashcards;
        AppUser = appUser;
    }

    public string Name { get; set; } = "";

    public List<Flashcard> Flashcards { get; set; } = new();

    public AppUser? AppUser { get; set; }
    public DateTime ModifiedDateTime { get; set; } = DateTime.UtcNow;
}