using Order.Domain.Common;

namespace Order.Domain.Events;

public sealed class OrderCancelledEvent : IDomainEvent
{
    public Guid OrderId { get; }
    public DateTime OccurredOn { get; }

    public OrderCancelledEvent(Guid orderId)
    {
        OrderId = orderId;
        OccurredOn = DateTime.UtcNow;
    }
}
