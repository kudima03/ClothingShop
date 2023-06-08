using ApplicationCore.Entities.BaseEntity;

namespace ApplicationCore.Entities;

public class OrderStatus : StorableEntity
{
    public string Name { get; set; }
    public virtual List<Order> Orders { get; init; } = new();
}