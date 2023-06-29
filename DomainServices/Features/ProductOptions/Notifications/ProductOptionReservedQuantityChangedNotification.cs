using MediatR;

namespace DomainServices.Features.ProductOptions.Notifications;
public class ProductOptionReservedQuantityChangedNotification : INotification
{
    public ProductOptionReservedQuantityChangedNotification(long productOptionId, long productId, int newReservedQuantity)
    {
        ProductOptionId = productOptionId;
        ProductId = productId;
        NewReservedQuantity = newReservedQuantity;
    }

    public long ProductOptionId { get; }
    public long ProductId { get; }
    public int NewReservedQuantity { get; }
}
