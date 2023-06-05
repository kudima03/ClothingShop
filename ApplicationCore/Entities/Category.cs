using ApplicationCore.Entities.Interfaces;

namespace ApplicationCore.Entities;

public class Category : IStorable
{
    public string Name { get; set; }
    public virtual List<Subcategory> Subcategories { get; init; } = new();
    public virtual List<Section> Sections { get; init; } = new();
    public long Id { get; set; }
}