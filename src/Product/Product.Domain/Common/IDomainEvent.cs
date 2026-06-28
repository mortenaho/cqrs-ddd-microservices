namespace Product.Domain.Common;

public interface IDomainEvent
{
    DateTime OccurredOn { get; }
}
