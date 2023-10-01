namespace DanilvarKanji.Attributes;

public class TodoAttribute : Attribute
{
    public string Message { get; }

    public TodoAttribute(string message) => 
        Message = message;
}