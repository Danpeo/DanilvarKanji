namespace DanilvarKanji.Domain.Persistance;

public interface IUnitOfWork
{
    Task<bool> CompleteAsync();
    bool HasChanges();
}