using ApplicationCore.Entities.BaseEntity;

namespace ApplicationCore.Entities;

public class Color : StorableEntity
{
    public string Name { get; set; }
    public string Hex { get; set; }
    public virtual List<ProductColor> ProductColors { get; init; } = new();
    public long Id { get; set; }
}