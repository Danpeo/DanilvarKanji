using DanilvarKanji.Attributes;

namespace DanilvarKanji.Utils;

public class Todo
{
    private readonly ILogger _logger;

    public Todo(ILogger logger)
    {
        _logger = logger;
    }

    public void LogAttributes()
    {
        var typesWithTodoAttributes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.GetCustomAttributes(typeof(TodoAttribute), true).Any())
            .ToList();

        foreach (Type type in typesWithTodoAttributes)
        {
            var todoAttributes = type.GetCustomAttributes(typeof(TodoAttribute), true).Cast<TodoAttribute>();
            foreach (TodoAttribute? todoAttribute in todoAttributes)
            {
                _logger.LogWarning($"[TODO] {type.Name}: {todoAttribute.Message}");
            }
        }
    }
}