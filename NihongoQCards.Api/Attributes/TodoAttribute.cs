namespace DanilvarKanji.Attributes;

public class TodoAttribute : Attribute
{
    public string Description { get; set; }

    public TodoAttribute(string description)
    {
        Description = description;
    }
}