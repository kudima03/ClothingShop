using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.ShoppingCartItems.Commands.Delete;

public class DeleteAndReturnToStockShoppingCartItemCommandHandler(IRepository<ShoppingCartItem> repository) : IRequestHandler<DeleteAndReturnToStockShoppingCartItemCommand, Unit>
{
    public async Task<Unit> Handle(DeleteAndReturnToStockShoppingCartItemCommand request, CancellationToken cancellationToken)
    {
        ShoppingCartItem? item = await repository.GetFirstOrDefaultAsync
                                     (x => x.Id == request.ShoppingCartItemId,
                                      x => x.Include(c => c.ProductOption),
                                      cancellationToken);

        if (item is not null)
        {
            item.ProductOption.Quantity += item.Amount;
            repository.Delete(item);
            await repository.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
    }
}