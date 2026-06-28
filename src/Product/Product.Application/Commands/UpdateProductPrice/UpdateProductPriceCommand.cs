using FluentValidation;
using MediatR;
using Product.Application.Interfaces;
using Product.Domain.ValueObjects;

namespace Product.Application.Commands.UpdateProductPrice;

public sealed record UpdateProductPriceCommand(
    Guid ProductId,
    decimal NewPrice,
    string Currency) : IRequest;

public sealed class UpdateProductPriceCommandValidator : AbstractValidator<UpdateProductPriceCommand>
{
    public UpdateProductPriceCommandValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();
        RuleFor(x => x.NewPrice).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Currency).NotEmpty().MaximumLength(3);
    }
}

public sealed class UpdateProductPriceCommandHandler : IRequestHandler<UpdateProductPriceCommand>
{
    private readonly IProductRepository _repository;

    public UpdateProductPriceCommandHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(UpdateProductPriceCommand request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(request.ProductId, cancellationToken)
            ?? throw new KeyNotFoundException($"Product with id '{request.ProductId}' was not found.");

        product.UpdatePrice(Money.Create(request.NewPrice, request.Currency));
        await _repository.UpdateAsync(product, cancellationToken);
    }
}
