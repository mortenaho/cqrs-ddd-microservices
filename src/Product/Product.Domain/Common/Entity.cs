namespace Product.Domain.Common;

/// <summary>
/// هر موجودیتی که با Id شناخته می‌شود — مثل یک ردیف در دیتابیس با کلید اصلی.
/// </summary>
public abstract class Entity
{
    public Guid Id { get; protected set; }

    protected Entity()
    {
    }
}
