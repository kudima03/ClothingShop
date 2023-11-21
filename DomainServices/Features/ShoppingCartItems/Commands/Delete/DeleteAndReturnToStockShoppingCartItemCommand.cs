using MediatR;

namespace DomainServices.Features.ShoppingCartItems.Commands.Delete;

public class DeleteAndReturnToStockShoppingCartItemCommand(long shoppingCartItemId) : IRequest<Unit>
{
    public long ShoppingCartItemId { get; init; } = shoppingCartItemId;
}