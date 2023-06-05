using ApplicationCore.Entities.Interfaces;

namespace ApplicationCore.Entities;

public class Order : IStorable
{
    public long UserId { get; set; }
    public virtual User User { get; set; }
    public long OrderStatusId { get; set; }
    public virtual OrderStatus OrderStatus { get; set; }
    public virtual List<ProductOption> ProductsOptions { get; init; } = new();
    public DateTime DateTime { get; set; }
    public long Id { get; set; }
}