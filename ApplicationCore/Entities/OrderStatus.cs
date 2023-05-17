namespace ApplicationCore.Entities;

public class OrderStatus
{
    public long Id { get; set; }
    public string Name { get; set; }
    public virtual List<Order> Orders { get; } = new();
}