using FluentValidation;
using MediatR;
using Order.Application.Interfaces;
using Order.Domain.Aggregates;

namespace Order.Application.Commands.CreateOrder;

public sealed record CreateOrderLineRequest(Guid ProductId, int Quantity);

public sealed record CreateOrderCommand(Guid UserId, IReadOnlyList<CreateOrderLineRequest> Lines) : IRequest<Guid>;

public sealed class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Lines).NotEmpty();
        RuleForEach(x => x.Lines).ChildRules(line =>
        {
            line.RuleFor(l => l.ProductId).NotEmpty();
            line.RuleFor(l => l.Quantity).GreaterThan(0);
        });
    }
}

public sealed class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUserServiceClient _userServiceClient;
    private readonly IProductServiceClient _productServiceClient;

    public CreateOrderCommandHandler(
        IOrderRepository orderRepository,
        IUserServiceClient userServiceClient,
        IProductServiceClient productServiceClient)
    {
        _orderRepository = orderRepository;
        _userServiceClient = userServiceClient;
        _productServiceClient = productServiceClient;
    }

    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var user = await _userServiceClient.GetUserAsync(request.UserId, cancellationToken)
            ?? throw new KeyNotFoundException($"User with id '{request.UserId}' was not found.");

        if (!user.IsActive)
        {
            throw new InvalidOperationException("Cannot create order for inactive user.");
        }

        var orderItems = new List<(Guid ProductId, string ProductName, int Quantity, decimal UnitPrice)>();

        foreach (var line in request.Lines)
        {
            var product = await _productServiceClient.GetProductAsync(line.ProductId, cancellationToken)
                ?? throw new KeyNotFoundException($"Product with id '{line.ProductId}' was not found.");

            if (product.StockQuantity < line.Quantity)
            {
                throw new InvalidOperationException(
                    $"Insufficient stock for product '{product.Name}'. Available: {product.StockQuantity}, Requested: {line.Quantity}.");
            }

            orderItems.Add((product.Id, product.Name, line.Quantity, product.Price));
        }

        var order = OrderAggregate.Create(request.UserId, orderItems);
        await _orderRepository.AddAsync(order, cancellationToken);
        return order.Id;
    }
}
