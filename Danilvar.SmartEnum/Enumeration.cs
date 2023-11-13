using System.Reflection;

namespace Danilvar.SmartEnum;

public abstract class Enumeration<TEnum> : IEquatable<Enumeration<TEnum>> where TEnum : Enumeration<TEnum>
{
    public int Value { get; protected init; }
    public string Name { get; protected init; } = string.Empty;

    private static readonly Dictionary<int, TEnum> Enumerations = CreateEnumerations();

    private static Dictionary<int, TEnum> CreateEnumerations()
    {
        Type enumType = typeof(TEnum);

        var fieldsForType = enumType
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(fieldInfo => enumType.IsAssignableFrom(fieldInfo.FieldType))
            .Select(fieldInfo => (TEnum)fieldInfo.GetValue(default)!);

        return fieldsForType.ToDictionary(x => x.Value);
    }

    protected Enumeration(int value, string name)
    {
        Value = value;
        Name = name;
    }

    public static TEnum? FromValue(int value)
    {
        return Enumerations.TryGetValue(
            value,
            out TEnum? enumeration)
            ? enumeration
            : default;
    }

    public static TEnum? FromName(string name) =>
        Enumerations
            .Values
            .SingleOrDefault(x => x.Name == name);

    public bool Equals(Enumeration<TEnum>? other)
    {
        if (other is null)
            return false;

        return GetType() == other.GetType() && Value == other.Value;
    }

    public override bool Equals(object? obj) =>
        obj is Enumeration<TEnum> other && Equals(other);

    public override int GetHashCode() =>
        Value.GetHashCode();

    public override string ToString() => Name;
}