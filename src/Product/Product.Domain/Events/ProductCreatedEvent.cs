using Product.Domain.Common;

namespace Product.Domain.Events;

public sealed class ProductCreatedEvent : IDomainEvent
{
    public Guid ProductId { get; }
    public string Name { get; }
    public DateTime OccurredOn { get; }

    public ProductCreatedEvent(Guid productId, string name)
    {
        ProductId = productId;
        Name = name;
        OccurredOn = DateTime.UtcNow;
    }
}
