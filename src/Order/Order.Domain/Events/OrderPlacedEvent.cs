using Order.Domain.Common;

namespace Order.Domain.Events;

public sealed class OrderPlacedEvent : IDomainEvent
{
    public Guid OrderId { get; }
    public Guid UserId { get; }
    public decimal TotalAmount { get; }
    public DateTime OccurredOn { get; }

    public OrderPlacedEvent(Guid orderId, Guid userId, decimal totalAmount)
    {
        OrderId = orderId;
        UserId = userId;
        TotalAmount = totalAmount;
        OccurredOn = DateTime.UtcNow;
    }
}
