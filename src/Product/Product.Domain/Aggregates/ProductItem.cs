using Product.Domain.Common;
using Product.Domain.Events;
using Product.Domain.ValueObjects;

namespace Product.Domain.Aggregates;

public sealed class ProductItem : AggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public Money Price { get; private set; } = Money.Create(0);
    public int StockQuantity { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private ProductItem()
    {
    }

    public static ProductItem Create(string name, string description, Money price, int stockQuantity)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("نام محصول الزامی است.", nameof(name));
        }

        if (stockQuantity < 0)
        {
            throw new ArgumentException("موجودی نمی‌تواند منفی باشد.", nameof(stockQuantity));
        }

        var product = new ProductItem
        {
            Id = Guid.NewGuid(),
            Name = name.Trim(),
            Description = description?.Trim() ?? string.Empty,
            Price = price,
            StockQuantity = stockQuantity,
            CreatedAt = DateTime.UtcNow
        };

        product.RaiseDomainEvent(new ProductCreatedEvent(product.Id, product.Name));
        return product;
    }

    public void UpdatePrice(Money newPrice)
    {
        Price = newPrice;
        UpdatedAt = DateTime.UtcNow;
        RaiseDomainEvent(new ProductPriceUpdatedEvent(Id, newPrice.Amount));
    }
}
