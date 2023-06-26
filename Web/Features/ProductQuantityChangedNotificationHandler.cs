using DomainServices.Features.ProductOptions.Notifications;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Web.Hubs;
using Web.Services.Interfaces;
using Web.SignalR;

namespace Web.Features;

public class ProductQuantityChangedNotificationHandler : INotificationHandler<ProductOptionQuantityChangedNotification>
{
    private readonly ISubscribesToProductsManager _subscribesToProductsManager;

    private readonly IHubContext<RealTimeProductInfoHub> _hubContext;

    public ProductQuantityChangedNotificationHandler(IHubContext<RealTimeProductInfoHub> hubContext,
        ISubscribesToProductsManager subscribesToProductsManager)
    {
        _hubContext = hubContext;
        _subscribesToProductsManager = subscribesToProductsManager;
    }

    public Task Handle(ProductOptionQuantityChangedNotification notification, CancellationToken cancellationToken)
    {
        IEnumerable<string> connectionsWatchingProduct = _subscribesToProductsManager
            .ConnectionsSubscribedToProduct(notification.ProductId);

        return _hubContext.Clients.Clients(connectionsWatchingProduct).SendAsync(SignalRConstants.ProductQuantityChanged,
            notification.NewQuantity, cancellationToken);
    }
}