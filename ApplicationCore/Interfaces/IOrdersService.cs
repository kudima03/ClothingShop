using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces;

public interface IOrdersService
{
    Task<Order> CreateOrder(long userId, ICollection<long> shoppingCartItemsIds, CancellationToken cancellationToken = default);

    Task CancelOrder(long orderId, CancellationToken cancellationToken = default);
}