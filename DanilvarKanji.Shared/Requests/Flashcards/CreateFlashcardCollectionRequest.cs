using DanilvarKanji.Shared.Domain.Entities.Flashcards;
using DVar.RandCreds;

namespace DanilvarKanji.Shared.Requests.Flashcards;

public class CreateFlashcardCollectionRequest
{
    public string Name { get; set; }

    public List<Flashcard> Flashcards { get; set; }

    public CreateFlashcardCollectionRequest()
    {
        Name = $"Collection {RandGen.PasswordDefault}";
        Flashcards = new List<Flashcard>();
    }
    
    public CreateFlashcardCollectionRequest(string name, List<Flashcard> flashcards)
    {
        Name = name;
        Flashcards = flashcards;
    }
}