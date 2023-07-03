using MediatR;

namespace DomainServices.Features.ShoppingCarts.Commands.Update;

public class UpdateShoppingCartCommand : IRequest<Unit>
{
    public long UserId { get; set; }

    public ICollection<UpdateShoppingCartDtos.ShoppingCartItemDto> ItemsDtos { get; init; } =
        new List<UpdateShoppingCartDtos.ShoppingCartItemDto>();
}