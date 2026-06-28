using User.Domain.Common;
using User.Domain.Events;
using User.Domain.ValueObjects;

namespace User.Domain.Aggregates;

public sealed class UserAccount : AggregateRoot
{
    public Email Email { get; private set; } = Email.Create("placeholder@example.com");
    public string FullName { get; private set; } = string.Empty;
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private UserAccount()
    {
    }

    public static UserAccount Register(Email email, string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
        {
            throw new ArgumentException("Full name is required.", nameof(fullName));
        }

        var user = new UserAccount
        {
            Id = Guid.NewGuid(),
            Email = email,
            FullName = fullName.Trim(),
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        user.RaiseDomainEvent(new UserRegisteredEvent(user.Id, user.Email.Value));
        return user;
    }

    public void Deactivate()
    {
        if (!IsActive)
        {
            throw new InvalidOperationException("User is already deactivated.");
        }

        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
        RaiseDomainEvent(new UserDeactivatedEvent(Id));
    }
}
