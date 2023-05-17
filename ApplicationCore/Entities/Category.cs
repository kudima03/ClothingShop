namespace ApplicationCore.Entities;

public class Category
{
    public long Id { get; set; }
    public string Name { get; set; }
    public virtual List<Subcategory> Subcategories { get; } = new();
    public virtual List<Section> SectionsBelongsTo { get; } = new();
}