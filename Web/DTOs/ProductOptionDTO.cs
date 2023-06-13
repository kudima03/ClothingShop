namespace Web.DTOs;

public class ProductOptionDTO
{
    public long Id { get; set; }
    public long ProductColorId { get; set; }
    public virtual ProductColorDTO ProductColor { get; set; }
    public string Size { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
