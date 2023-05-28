using ApplicationCore.Entities.Interfaces;

namespace ApplicationCore.Entities;

public class Color : IStorable
{
    public string Name { get; set; }
    public string Hex { get; set; }
    public virtual List<ProductColor> ProductColors { get; } = new();
    public long Id { get; set; }
}