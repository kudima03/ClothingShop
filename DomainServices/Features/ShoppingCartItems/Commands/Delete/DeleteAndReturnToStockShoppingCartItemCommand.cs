using MediatR;

namespace DomainServices.Features.ShoppingCartItems.Commands.Delete;

public class DeleteAndReturnToStockShoppingCartItemCommand : IRequest<Unit>
{
    public DeleteAndReturnToStockShoppingCartItemCommand(long shoppingCartItemId)
    {
        ShoppingCartItemId = shoppingCartItemId;
    }

    public long ShoppingCartItemId { get; init; }
}