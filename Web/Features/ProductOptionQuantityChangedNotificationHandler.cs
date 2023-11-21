using DomainServices.Features.ProductOptions.Notifications;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Web.Hubs;
using Web.Services.Interfaces;
using Web.SignalR;

namespace Web.Features;

public class ProductOptionQuantityChangedNotificationHandler(IHubContext<RealTimeProductInfoHub> hubContext,
                                                             ISubscribesToProductsManager subscribesToProductsManager)
    : INotificationHandler<ProductOptionQuantityChangedNotification>
{
    public async Task Handle(ProductOptionQuantityChangedNotification notification, CancellationToken cancellationToken)
    {
        IEnumerable<string> connectionsWatchingProduct = await subscribesToProductsManager
                                                             .GetConnectionsSubscribedToProductAsync(notification.ProductId);

        hubContext.Clients.Clients(connectionsWatchingProduct)
                   .SendAsync
                       (SignalRConstants.ProductOptionQuantityChanged,
                        notification.ProductOptionId,
                        notification.NewQuantity,
                        cancellationToken);
    }
}