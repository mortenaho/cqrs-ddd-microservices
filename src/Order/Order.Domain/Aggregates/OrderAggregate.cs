using Order.Domain.Common;
using Order.Domain.Entities;
using Order.Domain.Enums;
using Order.Domain.Events;

namespace Order.Domain.Aggregates;

public sealed class OrderAggregate : AggregateRoot
{
    private readonly List<OrderLine> _lines = [];

    public Guid UserId { get; private set; }
    public OrderStatus Status { get; private set; }
    public decimal TotalAmount { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public IReadOnlyCollection<OrderLine> Lines => _lines.AsReadOnly();

    private OrderAggregate()
    {
    }

    public static OrderAggregate Create(Guid userId, IEnumerable<(Guid ProductId, string ProductName, int Quantity, decimal UnitPrice)> items)
    {
        var lineItems = items.ToList();

        if (lineItems.Count == 0)
        {
            throw new ArgumentException("Order must contain at least one line item.");
        }

        var order = new OrderAggregate
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Status = OrderStatus.Confirmed,
            CreatedAt = DateTime.UtcNow
        };

        foreach (var item in lineItems)
        {
            order._lines.Add(OrderLine.Create(item.ProductId, item.ProductName, item.Quantity, item.UnitPrice));
        }

        order.TotalAmount = order._lines.Sum(l => l.LineTotal);
        order.RaiseDomainEvent(new OrderPlacedEvent(order.Id, order.UserId, order.TotalAmount));
        return order;
    }

    public void Cancel()
    {
        if (Status == OrderStatus.Cancelled)
        {
            throw new InvalidOperationException("Order is already cancelled.");
        }

        Status = OrderStatus.Cancelled;
        UpdatedAt = DateTime.UtcNow;
        RaiseDomainEvent(new OrderCancelledEvent(Id));
    }
}
