using Danilvar.Entity;

namespace DanilvarKanji.Domain.Shared.Entities.Flashcards;

public class Flashcard : Entity
{
  public Flashcard()
  {
    Main = "";
    Sub = "";
    Back = "";
  }

  public Flashcard(string back, string main, string sub)
  {
    Back = back;
    Main = main;
    Sub = sub;
  }

  public string Main { get; set; }

  public string Sub { get; set; }

  public string Back { get; set; }

  public bool DoRemember { get; set; }

  public int RememberedInARow { get; set; }
}