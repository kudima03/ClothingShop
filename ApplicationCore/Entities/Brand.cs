namespace ApplicationCore.Entities;

public class Brand
{
    public long Id { get; set; }
    public string Name { get; set; }
    public virtual List<Product> Products { get; } = new();
}