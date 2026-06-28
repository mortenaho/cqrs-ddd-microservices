namespace Order.Api.Controllers.Requests;

public sealed record CreateOrderRequest(Guid UserId, IReadOnlyList<CreateOrderLineApiRequest> Lines);

public sealed record CreateOrderLineApiRequest(Guid ProductId, int Quantity);
