using Danilvar.Entity;
using DanilvarKanji.Domain.Events;

namespace DanilvarKanji.Domain.Primitives;

public class DomainEntity : Entity
{
    private readonly List<IDomainEvent> _domainEvents = new();

    public IReadOnlyList<IDomainEvent> GetDomainEvents() => 
        _domainEvents.ToList();

    public void ClearDomainEvents() => 
        _domainEvents.Clear();

    protected void RaiseDomainEvent(IDomainEvent domainEvent) => 
        _domainEvents.Add(domainEvent);
}