using ApplicationCore.Entities;
using MediatR;

namespace DomainServices.Features.ShoppingCarts.Queries.GetByUserId;

public class GetShoppingCartByUserIdQuery(long userId) : IRequest<ShoppingCart>
{
    public long UserId { get; init; } = userId;
}