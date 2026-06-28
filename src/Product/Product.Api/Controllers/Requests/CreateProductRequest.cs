namespace Product.Api.Controllers.Requests;

public sealed record CreateProductRequest(
    string Name,
    string Description,
    decimal Price,
    string? Currency,
    int StockQuantity);

public sealed record UpdateProductPriceRequest(decimal NewPrice, string? Currency);
