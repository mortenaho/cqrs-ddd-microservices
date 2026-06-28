using FluentValidation;
using MediatR;
using Order.Application.Interfaces;

namespace Order.Application.Commands.CancelOrder;

public sealed record CancelOrderCommand(Guid OrderId) : IRequest;

public sealed class CancelOrderCommandValidator : AbstractValidator<CancelOrderCommand>
{
    public CancelOrderCommandValidator()
    {
        RuleFor(x => x.OrderId).NotEmpty();
    }
}

public sealed class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand>
{
    private readonly IOrderRepository _repository;

    public CancelOrderCommandHandler(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _repository.GetByIdAsync(request.OrderId, cancellationToken)
            ?? throw new KeyNotFoundException($"Order with id '{request.OrderId}' was not found.");

        order.Cancel();
        await _repository.UpdateAsync(order, cancellationToken);
    }
}
