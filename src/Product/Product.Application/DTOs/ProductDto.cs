namespace Product.Application.DTOs;

public sealed record ProductDto(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    string Currency,
    int StockQuantity,
    DateTime CreatedAt,
    DateTime? UpdatedAt);
