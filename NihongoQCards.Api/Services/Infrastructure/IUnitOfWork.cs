namespace DanilvarKanji.Services.Infrastructure;

public interface IUnitOfWork
{
    Task<bool> CompleteAsync();
    bool HasChanges();
}