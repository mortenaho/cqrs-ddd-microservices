using Product.Domain.Aggregates;

namespace Product.Application.Interfaces;

public interface IProductRepository
{
    Task<ProductItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ProductItem>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(ProductItem product, CancellationToken cancellationToken = default);
    Task UpdateAsync(ProductItem product, CancellationToken cancellationToken = default);
}
