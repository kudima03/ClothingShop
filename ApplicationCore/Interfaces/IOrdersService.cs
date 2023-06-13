using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces;
public interface IOrdersService
{
    Task<Order> CreateOrder(long userId, ICollection<ProductOptionIdAndQuantity> productOptionsIdsAndQuantity, CancellationToken cancellationToken = default);

    Task UpdateOrder(long orderId, ICollection<ProductOptionIdAndQuantity> productOptionsIdsAndQuantity, CancellationToken cancellationToken = default);

    Task CancelOrder(long orderId, CancellationToken cancellationToken = default);
}
