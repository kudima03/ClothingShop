using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces;
public interface IOrdersService
{
    Task<Order> CreateOrder(long userId, ICollection<OrderItemDto> orderItems, CancellationToken cancellationToken = default);

    Task UpdateOrder(long orderId, ICollection<OrderItemDto> orderItemsDtos, CancellationToken cancellationToken = default);

    Task CancelOrder(long orderId, CancellationToken cancellationToken = default);
}

public class OrderItemDto
{
    public long ProductOptionId { get; init; }
    public int Quantity { get; init; }
}