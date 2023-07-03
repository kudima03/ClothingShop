using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.ShoppingCarts.Queries.GetByUserId;

public class GetShoppingCartByUserIdQueryHandler : IRequestHandler<GetShoppingCartByUserIdQuery, ShoppingCart>
{
    private readonly IReadOnlyRepository<ShoppingCart?> _shoppingCartsRepository;

    public GetShoppingCartByUserIdQueryHandler(IReadOnlyRepository<ShoppingCart?> shoppingCartsRepository)
    {
        _shoppingCartsRepository = shoppingCartsRepository;
    }

    public async Task<ShoppingCart> Handle(GetShoppingCartByUserIdQuery request, CancellationToken cancellationToken)
    {
        return await _shoppingCartsRepository.GetFirstOrDefaultAsync
                   (x => x.UserId == request.UserId,
                    x => x.Include(shoppingCart => shoppingCart.Items)
                          .ThenInclude(cartItem => cartItem.ProductOption.ProductColor.ImagesInfos)
                          .Include(shoppingCart => shoppingCart.Items)
                          .ThenInclude(v => v.ProductOption.Product),
                    cancellationToken);
    }
}