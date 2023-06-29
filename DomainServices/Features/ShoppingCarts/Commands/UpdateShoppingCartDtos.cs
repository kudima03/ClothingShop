namespace DomainServices.Features.ShoppingCarts.Commands;
public static class UpdateShoppingCartDtos
{
    public class ShoppingCartItemDto
    {
        public long ProductOptionId { get; set; }
        public int Quantity { get; set; }
    }
}