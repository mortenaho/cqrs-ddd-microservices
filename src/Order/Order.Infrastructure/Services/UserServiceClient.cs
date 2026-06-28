using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using Order.Application.Interfaces;

namespace Order.Infrastructure.Services;

public sealed class UserServiceClient : IUserServiceClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<UserServiceClient> _logger;

    public UserServiceClient(HttpClient httpClient, ILogger<UserServiceClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<UserInfo?> GetUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _httpClient.GetAsync($"/api/users/{userId}", cancellationToken);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();
            var user = await response.Content.ReadFromJsonAsync<UserApiResponse>(cancellationToken);
            return user is null ? null : new UserInfo(user.Id, user.Email, user.FullName, user.IsActive);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get user {UserId} from User service", userId);
            throw;
        }
    }

    private sealed record UserApiResponse(
        Guid Id,
        string Email,
        string FullName,
        bool IsActive,
        DateTime CreatedAt,
        DateTime? UpdatedAt);
}
