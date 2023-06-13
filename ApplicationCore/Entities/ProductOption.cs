using ApplicationCore.Entities.BaseEntity;

namespace ApplicationCore.Entities;

public class ProductOption : StorableEntity
{
    public long ProductId { get; set; }
    public virtual Product Product { get; set; }
    public long ProductColorId { get; set; }
    public virtual ProductColor ProductColor { get; set; }
    public string Size { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public virtual List<OrderedProductOption> OrderedProductOptions { get; init; } = new();
}