using Danilvar.Entity;

namespace DanilvarKanji.Shared.Domain.Entities.Flashcards;

public class FlashcardCollection : Entity
{
    public string Name { get; set; }

    public List<Flashcard> Flashcards { get; set; }

    public AppUser AppUser { get; set; }

    public FlashcardCollection()
    {
        
    }
    
    public FlashcardCollection(string name, List<Flashcard> flashcards, AppUser appUser)
    {
        Name = name;
        Flashcards = flashcards;
        AppUser = appUser;
    }
}