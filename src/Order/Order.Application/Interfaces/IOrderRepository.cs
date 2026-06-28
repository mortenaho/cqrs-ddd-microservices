using Order.Domain.Aggregates;

namespace Order.Application.Interfaces;

public interface IOrderRepository
{
    Task<OrderAggregate?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<OrderAggregate>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task AddAsync(OrderAggregate order, CancellationToken cancellationToken = default);
    Task UpdateAsync(OrderAggregate order, CancellationToken cancellationToken = default);
}
