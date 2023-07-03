namespace DomainServices.Features.Products.Commands;

public static class CreateUpdateProductCommandsDtos
{
    public class ImageInfoDto
    {
        public long Id { get; set; }

        public string Url { get; set; }
    }

    public class ProductColorDto
    {
        public long Id { get; set; }

        public string ColorHex { get; set; }

        public ICollection<ImageInfoDto> ImagesInfos { get; init; } = new List<ImageInfoDto>();
    }

    public class ProductOptionDto
    {
        public long Id { get; set; }

        public long ProductColorId { get; set; }

        public virtual ProductColorDto ProductColor { get; set; }

        public string Size { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}