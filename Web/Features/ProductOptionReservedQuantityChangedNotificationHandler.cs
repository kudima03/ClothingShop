using DomainServices.Features.ProductOptions.Notifications;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Web.Hubs;
using Web.Services.Interfaces;
using Web.SignalR;

namespace Web.Features;

public class ProductOptionReservedQuantityChangedNotificationHandler(ISubscribesToProductsManager subscribesToProductsManager,
                                                                     IHubContext<RealTimeProductInfoHub> hubContext)
    : INotificationHandler<ProductOptionReservedQuantityChangedNotification>
{
    public async Task Handle(ProductOptionReservedQuantityChangedNotification notification, CancellationToken cancellationToken)
    {
        IEnumerable<string> connectionsWatchingProduct = await subscribesToProductsManager
                                                             .GetConnectionsSubscribedToProductAsync(notification.ProductId);

        hubContext.Clients.Clients(connectionsWatchingProduct)
                   .SendAsync
                       (SignalRConstants.ProductOptionReservedQuantityChanged,
                        notification.ProductOptionId,
                        notification.NewReservedQuantity,
                        cancellationToken);
    }
}