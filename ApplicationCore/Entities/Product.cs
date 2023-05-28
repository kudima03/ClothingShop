using ApplicationCore.Entities.Interfaces;

namespace ApplicationCore.Entities;

public class Product : IStorable
{
    public long BrandId { get; set; }
    public virtual Brand Brand { get; set; }
    public long SubcategoryId { get; set; }
    public virtual Subcategory Subcategory { get; set; }
    public string Name { get; set; }
    public virtual List<ProductOption> ProductOptions { get; } = new();
    public virtual List<Review> Reviews { get; } = new();
    public long Id { get; set; }
}