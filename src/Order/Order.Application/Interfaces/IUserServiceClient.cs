namespace Order.Application.Interfaces;

public sealed record UserInfo(
    Guid Id,
    string Email,
    string FullName,
    bool IsActive);

public interface IUserServiceClient
{
    Task<UserInfo?> GetUserAsync(Guid userId, CancellationToken cancellationToken = default);
}
