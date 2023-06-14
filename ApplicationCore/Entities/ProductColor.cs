using ApplicationCore.Entities.BaseEntity;

namespace ApplicationCore.Entities;

public class ProductColor : StorableEntity
{
    public virtual List<ImageInfo> ImagesInfos { get; init; } = new();
    public virtual List<ProductOption> ProductOptions { get; init; } = new();
    public string ColorHex { get; set; }
}