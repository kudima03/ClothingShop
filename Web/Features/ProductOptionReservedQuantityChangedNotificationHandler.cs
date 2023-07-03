using DomainServices.Features.ProductOptions.Notifications;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Web.Hubs;
using Web.Services.Interfaces;
using Web.SignalR;

namespace Web.Features;

public class ProductOptionReservedQuantityChangedNotificationHandler
    : INotificationHandler<ProductOptionReservedQuantityChangedNotification>
{
    private readonly IHubContext<RealTimeProductInfoHub> _hubContext;
    private readonly ISubscribesToProductsManager _subscribesToProductsManager;

    public ProductOptionReservedQuantityChangedNotificationHandler(ISubscribesToProductsManager subscribesToProductsManager,
                                                                   IHubContext<RealTimeProductInfoHub> hubContext)
    {
        _subscribesToProductsManager = subscribesToProductsManager;
        _hubContext = hubContext;
    }

    public Task Handle(ProductOptionReservedQuantityChangedNotification notification, CancellationToken cancellationToken)
    {
        IEnumerable<string> connectionsWatchingProduct = _subscribesToProductsManager
            .ConnectionsSubscribedToProduct(notification.ProductId);

        return _hubContext.Clients.Clients(connectionsWatchingProduct)
                          .SendAsync
                              (SignalRConstants.ProductOptionReservedQuantityChanged,
                               notification.ProductOptionId,
                               notification.NewReservedQuantity,
                               cancellationToken);
    }
}