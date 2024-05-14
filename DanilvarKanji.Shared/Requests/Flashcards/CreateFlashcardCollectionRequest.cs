using DanilvarKanji.Domain.Shared.Entities.Flashcards;
using DVar.RandCreds;

namespace DanilvarKanji.Shared.Requests.Flashcards;

public class CreateFlashcardCollectionRequest
{
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

    public string Name { get; set; }

    public List<Flashcard> Flashcards { get; set; }
}