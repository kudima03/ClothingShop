namespace Web.DTOs;

public class ProductDTO
{
    public long Id { get; set; }
    public long BrandId { get; init; }
    public long SubcategoryId { get; init; }
    public string Name { get; init; }
    public ICollection<ProductOptionDTO> ProductOptions { get; init; } = new List<ProductOptionDTO>();
}
