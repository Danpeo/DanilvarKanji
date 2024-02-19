using Danilvar.Entity;

namespace DanilvarKanji.Shared.Domain.Entities.Flashcards;

public class Front : Entity
{
    public string Main { get; set; }

    public string Sub { get; set; }

    public Front()
    {
        
    }
    
    public Front(string main, string sub = "")
    {
        Main = main;
        Sub = sub;
    }
}