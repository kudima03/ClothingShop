using ApplicationCore.Entities.BaseEntity;

namespace ApplicationCore.Entities;
public class ShoppingCartItem : StorableEntity
{
    public long ShoppingCartId { get; set; }
    public ShoppingCart ShoppingCart { get; set; }
    public long ProductOptionId { get; set; }
    public ProductOption ProductOption { get; set; }
    public int Amount { get; set; }
    public DateTime CreationDateTime { get; set; } = DateTime.UtcNow;
}