using Microsoft.EntityFrameworkCore;
using Product.Application.Interfaces;
using Product.Domain.Aggregates;
using Product.Infrastructure.Persistence;

namespace Product.Infrastructure.Repositories;

public sealed class ProductRepository : IProductRepository
{
    private readonly ProductDbContext _context;

    public ProductRepository(ProductDbContext context)
    {
        _context = context;
    }

    public async Task<ProductItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<ProductItem>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ProductItem product, CancellationToken cancellationToken = default)
    {
        await _context.Products.AddAsync(product, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        product.ClearDomainEvents();
    }

    public async Task UpdateAsync(ProductItem product, CancellationToken cancellationToken = default)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync(cancellationToken);
        product.ClearDomainEvents();
    }
}
