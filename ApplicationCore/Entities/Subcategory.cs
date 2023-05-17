namespace ApplicationCore.Entities;

public class Subcategory
{
    public long Id { get; set; }
    public string Name { get; set; }
    public virtual List<Category> CategoriesBelongsTo { get; } = new();
    public virtual List<Product> Products { get; } = new();
}