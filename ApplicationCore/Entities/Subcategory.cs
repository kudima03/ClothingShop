using ApplicationCore.Entities.Interfaces;

namespace ApplicationCore.Entities;

public class Subcategory : IStorable
{
    public string Name { get; set; }
    public virtual List<Category> Categories { get; init; } = new();
    public virtual List<Product> Products { get; init; } = new();
    public long Id { get; set; }
}