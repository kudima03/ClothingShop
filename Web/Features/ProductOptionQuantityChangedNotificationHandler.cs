using DomainServices.Features.ProductOptions.Notifications;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Web.Hubs;
using Web.Services.Interfaces;
using Web.SignalR;

namespace Web.Features;

public class ProductOptionQuantityChangedNotificationHandler : INotificationHandler<ProductOptionQuantityChangedNotification>
{
    private readonly ISubscribesToProductsManager _subscribesToProductsManager;

    private readonly IHubContext<RealTimeProductInfoHub> _hubContext;

    public ProductOptionQuantityChangedNotificationHandler(IHubContext<RealTimeProductInfoHub> hubContext,
        ISubscribesToProductsManager subscribesToProductsManager)
    {
        _hubContext = hubContext;
        _subscribesToProductsManager = subscribesToProductsManager;
    }

    public Task Handle(ProductOptionQuantityChangedNotification notification, CancellationToken cancellationToken)
    {
        IEnumerable<string> connectionsWatchingProduct = _subscribesToProductsManager
            .ConnectionsSubscribedToProduct(notification.ProductId);

        return _hubContext.Clients.Clients(connectionsWatchingProduct).SendAsync(
            SignalRConstants.ProductOptionQuantityChanged,
            notification.ProductOptionId, notification.NewQuantity, cancellationToken);
    }
}