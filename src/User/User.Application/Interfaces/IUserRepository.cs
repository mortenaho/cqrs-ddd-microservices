using User.Domain.Aggregates;
using User.Domain.ValueObjects;

namespace User.Application.Interfaces;

public interface IUserRepository
{
    Task<UserAccount?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<UserAccount?> GetByEmailAsync(Email email, CancellationToken cancellationToken = default);
    Task<bool> EmailExistsAsync(Email email, CancellationToken cancellationToken = default);
    Task AddAsync(UserAccount user, CancellationToken cancellationToken = default);
    Task UpdateAsync(UserAccount user, CancellationToken cancellationToken = default);
}
