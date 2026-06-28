using User.Domain.Common;

namespace User.Domain.Events;

public sealed class UserDeactivatedEvent : IDomainEvent
{
    public Guid UserId { get; }
    public DateTime OccurredOn { get; }

    public UserDeactivatedEvent(Guid userId)
    {
        UserId = userId;
        OccurredOn = DateTime.UtcNow;
    }
}
