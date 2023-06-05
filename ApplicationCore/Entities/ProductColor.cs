using ApplicationCore.Entities.BaseEntity;

namespace ApplicationCore.Entities;

public class ProductColor : StorableEntity
{
    public long ColorId { get; set; }
    public virtual Color Color { get; set; }
    public virtual List<ImageInfo> ImagesInfos { get; init; } = new();
    public virtual List<ProductOption> ProductOptions { get; init; } = new();
    public long Id { get; set; }
}