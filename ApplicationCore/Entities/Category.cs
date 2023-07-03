using ApplicationCore.Entities.BaseEntity;

namespace ApplicationCore.Entities;

public class Category : StorableEntity
{
    public string Name { get; set; }

    public virtual List<Subcategory> Subcategories { get; init; } = new List<Subcategory>();

    public virtual List<Section> Sections { get; init; } = new List<Section>();
}