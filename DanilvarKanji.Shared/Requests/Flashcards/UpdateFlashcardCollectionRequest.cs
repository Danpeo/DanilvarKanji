using DanilvarKanji.Shared.Domain.Entities.Flashcards;
using DVar.RandCreds;

namespace DanilvarKanji.Shared.Requests.Flashcards;

public class UpdateFlashcardCollectionRequest
{
    public string CollectionToUpdateId { get; set; }
    public string Name { get; set; }

    public List<Flashcard> Flashcards { get; set; }

    public UpdateFlashcardCollectionRequest()
    {
        CollectionToUpdateId = "";
        Name = $"Collection {RandGen.PasswordDefault}";
        Flashcards = new List<Flashcard>();
    }
    
    public UpdateFlashcardCollectionRequest(string name, List<Flashcard> flashcards, string collectionToUpdateId)
    {
        Name = name;
        Flashcards = flashcards;
        CollectionToUpdateId = collectionToUpdateId;
    }
}