using ApplicationCore.Entities.Interfaces;

namespace ApplicationCore.Entities;

public class Category : IStorable
{
    public string Name { get; set; }
    public virtual List<Subcategory> Subcategories { get; } = new();
    public virtual List<Section> Sections { get; } = new();
    public long Id { get; set; }
}