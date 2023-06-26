using MediatR;

namespace DomainServices.Features.ProductOptions.Notifications;

public class ProductOptionQuantityChangedNotification : INotification
{

    public ProductOptionQuantityChangedNotification(long productOptionId, long productId, int newQuantity)
    {
        ProductId = productId;
        NewQuantity = newQuantity;
        ProductOptionId = productOptionId;
    }
    public long ProductOptionId { get; }
    public long ProductId { get; }
    public int NewQuantity { get; }
}
