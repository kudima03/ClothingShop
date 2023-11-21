using MediatR;

namespace DomainServices.Features.ProductOptions.Notifications;

public class ProductOptionReservedQuantityChangedNotification
    (long productOptionId, long productId, int newReservedQuantity) : INotification
{
    public long ProductOptionId { get; } = productOptionId;

    public long ProductId { get; } = productId;

    public int NewReservedQuantity { get; } = newReservedQuantity;
}