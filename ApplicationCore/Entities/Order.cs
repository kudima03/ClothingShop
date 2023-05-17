namespace ApplicationCore.Entities;

public class Order
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public virtual User User { get; set; }
    public long OrderStatusId { get; set; }
    public virtual OrderStatus OrderStatus { get; set; }
    public virtual List<ProductOption> ProductsOptions { get; } = new();
    public DateTime DateTime { get; set; }
}