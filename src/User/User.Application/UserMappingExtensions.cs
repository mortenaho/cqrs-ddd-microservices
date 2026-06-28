using User.Application.DTOs;
using User.Domain.Aggregates;

namespace User.Application;

internal static class UserMappingExtensions
{
    internal static UserDto ToDto(this UserAccount user) =>
        new(user.Id, user.Email.Value, user.FullName, user.IsActive, user.CreatedAt, user.UpdatedAt);
}
