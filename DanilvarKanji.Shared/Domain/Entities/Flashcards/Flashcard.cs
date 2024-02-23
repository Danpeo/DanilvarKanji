using Danilvar.Entity;

namespace DanilvarKanji.Shared.Domain.Entities.Flashcards;

public class Flashcard : Entity
{
    public Front Front { get; set; }

    public string Back { get; set; }

    public bool DoRemember { get; set; }

    public int RememberedInARow { get; set; }

    public Flashcard()
    {
        Front = new Front();
        Back = "";
    }
    
    public Flashcard(Front front, string back)
    {
        Front = front;
        Back = back;
    }
}
