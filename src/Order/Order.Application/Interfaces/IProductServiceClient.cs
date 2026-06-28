namespace Order.Application.Interfaces;

public sealed record ProductInfo(
    Guid Id,
    string Name,
    decimal Price,
    string Currency,
    int StockQuantity);

public interface IProductServiceClient
{
    Task<ProductInfo?> GetProductAsync(Guid productId, CancellationToken cancellationToken = default);
}
