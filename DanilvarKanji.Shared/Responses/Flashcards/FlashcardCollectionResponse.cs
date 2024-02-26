using DanilvarKanji.Shared.Domain.Entities.Flashcards;

namespace DanilvarKanji.Shared.Responses.Flashcards;

public class FlashcardCollectionResponse
{
    public string Id { get; set; }

    public string Name { get; set; }

    public List<Flashcard> Flashcards { get; set; }

    public FlashcardCollectionResponse(string id, string name, List<Flashcard> flashcards)
    {
        Id = id;
        Name = name;
        Flashcards = flashcards;
    }
}