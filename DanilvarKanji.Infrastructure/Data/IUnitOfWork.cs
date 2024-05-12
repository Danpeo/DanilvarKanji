namespace DanilvarKanji.Infrastructure.Data;

public interface IUnitOfWork
{
  Task<bool> CompleteAsync();
  bool HasChanges();
}