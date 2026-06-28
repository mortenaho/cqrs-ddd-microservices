using Order.Domain.Enums;

namespace Order.Application.DTOs;

public sealed record OrderLineDto(
    Guid Id,
    Guid ProductId,
    string ProductName,
    int Quantity,
    decimal UnitPrice,
    decimal LineTotal);

public sealed record OrderDto(
    Guid Id,
    Guid UserId,
    OrderStatus Status,
    decimal TotalAmount,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    IReadOnlyList<OrderLineDto> Lines);
