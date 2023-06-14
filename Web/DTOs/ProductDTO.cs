namespace Web.DTOs;

public class ProductDto
{
    public long Id { get; set; }
    public long BrandId { get; init; }
    public long SubcategoryId { get; init; }
    public string Name { get; init; }
    public ICollection<ProductOptionDto> ProductOptions { get; init; } = new List<ProductOptionDto>();
}
