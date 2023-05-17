namespace ApplicationCore.Entities;

public class ProductColor
{
    public long Id { get; set; }
    public long ColorId { get; set; }
    public virtual Color Color { get; set; }
    public virtual List<ImageInfo> ImagesInfos { get; } = new();
    public virtual List<ProductOption> ProductOptions { get; } = new();
}