using ApplicationCore.Entities.BaseEntity;

namespace ApplicationCore.Entities;

public class ShoppingCart : StorableEntity
{
    public long UserId { get; set; }

    public List<ShoppingCartItem> Items { get; init; } = new List<ShoppingCartItem>();
}