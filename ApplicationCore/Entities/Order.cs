using ApplicationCore.Entities.BaseEntity;

namespace ApplicationCore.Entities;

public class Order : StorableEntity
{
    public long UserId { get; set; }

    public virtual User User { get; set; }

    public long OrderStatusId { get; set; }

    public virtual OrderStatus OrderStatus { get; set; }

    public virtual List<OrderItem> OrderedProductsOptionsInfo { get; init; } = new();

    public DateTime DateTime { get; set; }
}