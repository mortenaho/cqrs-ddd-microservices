using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using Order.Application.Interfaces;

namespace Order.Infrastructure.Services;

public sealed class ProductServiceClient : IProductServiceClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ProductServiceClient> _logger;

    public ProductServiceClient(HttpClient httpClient, ILogger<ProductServiceClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<ProductInfo?> GetProductAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _httpClient.GetAsync($"/api/products/{productId}", cancellationToken);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();
            var product = await response.Content.ReadFromJsonAsync<ProductApiResponse>(cancellationToken);
            return product is null
                ? null
                : new ProductInfo(product.Id, product.Name, product.Price, product.Currency, product.StockQuantity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get product {ProductId} from Product service", productId);
            throw;
        }
    }

    private sealed record ProductApiResponse(
        Guid Id,
        string Name,
        string Description,
        decimal Price,
        string Currency,
        int StockQuantity,
        DateTime CreatedAt,
        DateTime? UpdatedAt);
}
