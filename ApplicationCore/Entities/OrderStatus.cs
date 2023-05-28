using ApplicationCore.Entities.Interfaces;

namespace ApplicationCore.Entities;

public class OrderStatus : IStorable
{
    public string Name { get; set; }
    public virtual List<Order> Orders { get; } = new();
    public long Id { get; set; }
}