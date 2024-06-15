using DanilvarKanji.Domain.Shared.Enumerations;

namespace DanilvarKanji.Domain.Primitives;

public interface ILocalizable
{
    public Culture Culture { get; init; }
}