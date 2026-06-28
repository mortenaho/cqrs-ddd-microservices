using Product.Application.DTOs;
using Product.Domain.Aggregates;

namespace Product.Application;

internal static class ProductMappingExtensions
{
    internal static ProductDto ToDto(this ProductItem product) =>
        new(
            product.Id,
            product.Name,
            product.Description,
            product.Price.Amount,
            product.Price.Currency,
            product.StockQuantity,
            product.CreatedAt,
            product.UpdatedAt);
}
