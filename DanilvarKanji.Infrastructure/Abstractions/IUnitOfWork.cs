namespace DanilvarKanji.Infrastructure.Abstractions;

public interface IUnitOfWork
{
    Task<bool> CompleteAsync();
    bool HasChanges();
}