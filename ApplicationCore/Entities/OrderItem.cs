using ApplicationCore.Entities.BaseEntity;

namespace ApplicationCore.Entities;

public class OrderItem : StorableEntity
{
    public long OrderId { get; set; }

    public Order Order { get; set; }

    public long ProductOptionId { get; set; }

    public ProductOption ProductOption { get; set; }

    public int Amount { get; set; }
}