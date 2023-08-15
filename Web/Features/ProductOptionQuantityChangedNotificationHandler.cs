using DomainServices.Features.ProductOptions.Notifications;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Web.Hubs;
using Web.Services.Interfaces;
using Web.SignalR;

namespace Web.Features;

public class ProductOptionQuantityChangedNotificationHandler : INotificationHandler<ProductOptionQuantityChangedNotification>
{
    private readonly IHubContext<RealTimeProductInfoHub> _hubContext;
    private readonly ISubscribesToProductsManager _subscribesToProductsManager;

    public ProductOptionQuantityChangedNotificationHandler(IHubContext<RealTimeProductInfoHub> hubContext,
                                                           ISubscribesToProductsManager subscribesToProductsManager)
    {
        _hubContext = hubContext;
        _subscribesToProductsManager = subscribesToProductsManager;
    }

    public async Task Handle(ProductOptionQuantityChangedNotification notification, CancellationToken cancellationToken)
    {
        IEnumerable<string> connectionsWatchingProduct = await _subscribesToProductsManager
                                                             .GetConnectionsSubscribedToProductAsync(notification.ProductId);

        _hubContext.Clients.Clients(connectionsWatchingProduct)
                   .SendAsync
                       (SignalRConstants.ProductOptionQuantityChanged,
                        notification.ProductOptionId,
                        notification.NewQuantity,
                        cancellationToken);
    }
}