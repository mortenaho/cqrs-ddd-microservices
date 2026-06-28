using FluentValidation;
using MediatR;
using Order.Application.DTOs;
using Order.Application.Interfaces;

namespace Order.Application.Queries.GetOrdersByUser;

public sealed record GetOrdersByUserQuery(Guid UserId) : IRequest<IReadOnlyList<OrderDto>>;

public sealed class GetOrdersByUserQueryValidator : AbstractValidator<GetOrdersByUserQuery>
{
    public GetOrdersByUserQueryValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
    }
}

public sealed class GetOrdersByUserQueryHandler : IRequestHandler<GetOrdersByUserQuery, IReadOnlyList<OrderDto>>
{
    private readonly IOrderRepository _repository;

    public GetOrdersByUserQueryHandler(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<OrderDto>> Handle(GetOrdersByUserQuery request, CancellationToken cancellationToken)
    {
        var orders = await _repository.GetByUserIdAsync(request.UserId, cancellationToken);
        return orders.Select(o => o.ToDto()).ToList();
    }
}
