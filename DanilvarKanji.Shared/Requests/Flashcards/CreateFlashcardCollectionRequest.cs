using DanilvarKanji.Shared.Domain.Entities;
using DanilvarKanji.Shared.Domain.Entities.Flashcards;

namespace DanilvarKanji.Shared.Requests.Flashcards;

public class CreateFlashcardCollectionRequest
{
    public string Name { get; set; }

    public List<Flashcard> Flashcards { get; set; }


    public CreateFlashcardCollectionRequest(string name, List<Flashcard> flashcards)
    {
        Name = name;
        Flashcards = flashcards;
    }
}