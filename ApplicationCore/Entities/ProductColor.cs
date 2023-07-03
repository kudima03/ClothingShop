using ApplicationCore.Entities.BaseEntity;

namespace ApplicationCore.Entities;

public class ProductColor : StorableEntity
{
    public virtual List<ImageInfo> ImagesInfos { get; init; } = new List<ImageInfo>();

    public virtual List<ProductOption> ProductOptions { get; init; } = new List<ProductOption>();

    public string ColorHex { get; set; }
}