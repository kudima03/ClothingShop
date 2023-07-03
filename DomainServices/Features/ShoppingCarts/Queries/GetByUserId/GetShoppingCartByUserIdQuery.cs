using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.ShoppingCarts.Queries.GetByUserId;

public class GetShoppingCartByUserIdQuery : IRequest<ShoppingCart>
{
    public GetShoppingCartByUserIdQuery(long userId)
    {
        UserId = userId;
    }

    public long UserId { get; init; }
}