namespace Product.Domain.Common;

/// <summary>
/// ریشه Aggregate — رئیس گروه. فقط از طریق او باید تغییر ایجاد شود.
/// علاوه بر Id، می‌تواند Domain Event ثبت کند.
/// </summary>
public abstract class AggregateRoot : Entity
{
    private readonly List<IDomainEvent> _domainEvents = [];

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void RaiseDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);

    public void ClearDomainEvents() => _domainEvents.Clear();
}
