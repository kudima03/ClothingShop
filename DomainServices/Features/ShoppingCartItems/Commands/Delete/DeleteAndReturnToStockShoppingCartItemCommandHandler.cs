using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Features.ShoppingCartItems.Commands.Delete;
public class DeleteAndReturnToStockShoppingCartItemCommandHandler : IRequestHandler<DeleteAndReturnToStockShoppingCartItemCommand, Unit>
{
    private readonly IRepository<ShoppingCartItem> _repository;

    public DeleteAndReturnToStockShoppingCartItemCommandHandler(IRepository<ShoppingCartItem> repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeleteAndReturnToStockShoppingCartItemCommand request, CancellationToken cancellationToken)
    {
        ShoppingCartItem? item = await _repository.GetFirstOrDefaultAsync(predicate: x => x.Id == request.ShoppingCartItemId,
            include: x => x.Include(c => c.ProductOption),
            cancellationToken: cancellationToken);

        if (item is not null)
        {
            item.ProductOption.Quantity += item.Amount;
            _repository.Delete(item);
            await _repository.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
    }
}
