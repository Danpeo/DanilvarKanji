using System.Reflection;

namespace DanilvarKanji.Client.Validation;

public static class ErrorHandler
{
    public static TRequest? HandleLists<TRequest>(IError? error)
    {
        if (error is null)
            return default;

        var properties = error.GetType().GetProperties();

        foreach (PropertyInfo property in properties)
        {
            if (property.PropertyType != typeof(List<string>))
                continue;
            var errorList = (List<string>)property.GetValue(error);

            if (errorList != null)
            {
                string completeMessage = errorList.Aggregate("", (current, errorMessage) => current + errorMessage);
                throw new HttpRequestException($"{completeMessage}");
            }
        }

        return default;
    }
}