namespace User.Application.DTOs;

public sealed record UserDto(
    Guid Id,
    string Email,
    string FullName,
    bool IsActive,
    DateTime CreatedAt,
    DateTime? UpdatedAt);
