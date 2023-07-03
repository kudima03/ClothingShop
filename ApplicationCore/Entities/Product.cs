using ApplicationCore.Entities.BaseEntity;

namespace ApplicationCore.Entities;

public class Product : StorableEntity
{
    public long BrandId { get; set; }

    public virtual Brand Brand { get; set; }

    public long SubcategoryId { get; set; }

    public virtual Subcategory Subcategory { get; set; }

    public string Name { get; set; }

    public virtual List<ProductOption> ProductOptions { get; init; } = new List<ProductOption>();

    public virtual List<Review> Reviews { get; init; } = new List<Review>();
}