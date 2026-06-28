using Product.Domain.Common;

namespace Product.Domain.Events;

public sealed class ProductPriceUpdatedEvent : IDomainEvent
{
    public Guid ProductId { get; }
    public decimal NewAmount { get; }
    public DateTime OccurredOn { get; }

    public ProductPriceUpdatedEvent(Guid productId, decimal newAmount)
    {
        ProductId = productId;
        NewAmount = newAmount;
        OccurredOn = DateTime.UtcNow;
    }
}
