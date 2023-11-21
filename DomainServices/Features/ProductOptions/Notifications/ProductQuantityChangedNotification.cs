using MediatR;

namespace DomainServices.Features.ProductOptions.Notifications;

public class ProductOptionQuantityChangedNotification(long productOptionId, long productId, int newQuantity) : INotification
{
    public long ProductOptionId { get; } = productOptionId;

    public long ProductId { get; } = productId;

    public int NewQuantity { get; } = newQuantity;
}