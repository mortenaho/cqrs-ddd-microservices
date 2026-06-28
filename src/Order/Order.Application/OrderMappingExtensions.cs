using Order.Application.DTOs;
using Order.Domain.Aggregates;
using Order.Domain.Entities;

namespace Order.Application;

internal static class OrderMappingExtensions
{
    internal static OrderDto ToDto(this OrderAggregate order) =>
        new(
            order.Id,
            order.UserId,
            order.Status,
            order.TotalAmount,
            order.CreatedAt,
            order.UpdatedAt,
            order.Lines.Select(l => l.ToDto()).ToList());

    private static OrderLineDto ToDto(this OrderLine line) =>
        new(line.Id, line.ProductId, line.ProductName, line.Quantity, line.UnitPrice, line.LineTotal);
}
