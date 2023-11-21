using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.ShoppingCarts.Queries.GetByUserId;

public class GetShoppingCartByUserIdQueryHandler(IReadOnlyRepository<ShoppingCart?> shoppingCartsRepository) : IRequestHandler<GetShoppingCartByUserIdQuery, ShoppingCart>
{
    public async Task<ShoppingCart> Handle(GetShoppingCartByUserIdQuery request, CancellationToken cancellationToken)
    {
        return await shoppingCartsRepository.GetFirstOrDefaultAsync
                   (x => x.UserId == request.UserId,
                    x => x.Include(shoppingCart => shoppingCart.Items)
                          .ThenInclude(cartItem => cartItem.ProductOption.ProductColor.ImagesInfos)
                          .Include(shoppingCart => shoppingCart.Items)
                          .ThenInclude(v => v.ProductOption.Product.Brand),
                    cancellationToken);
    }
}