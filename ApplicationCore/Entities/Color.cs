namespace ApplicationCore.Entities;

public class Color
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Hex { get; set; }
    public virtual List<ProductColor> ProductColors { get; } = new();
}