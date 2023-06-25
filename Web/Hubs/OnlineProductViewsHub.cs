using Infrastructure.Identity.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Web.Services.Interfaces;
using Web.SignalR;

namespace Web.Hubs;

[Authorize(Policy = PolicyName.Customer)]
public class OnlineProductViewsHub : Hub
{
    private readonly IConnectionsToProductsManager _connectionsManager;

    public OnlineProductViewsHub(IConnectionsToProductsManager connectionsManager)
    {
        _connectionsManager = connectionsManager;
    }

    public async Task GetProductWatchersCount(long productId)
    {
        int watchersBeforeNewConnection = _connectionsManager.WatchingProductUsersCount(productId);
        long userId = long.Parse(Context.UserIdentifier!);
        _connectionsManager.AddConnection(userId, productId, Context.ConnectionId);
        int watchersAfterNewConnection = _connectionsManager.WatchingProductUsersCount(productId);
        bool productWatchersAmountChanged = watchersAfterNewConnection != watchersBeforeNewConnection;
        
        if (productWatchersAmountChanged)
        {
            IEnumerable<string> connectionsWatchingProduct = _connectionsManager.ConnectionsAssociatedWithProduct(productId);
            await Clients.Clients(connectionsWatchingProduct).SendAsync(SignalRConstants.ProductWatchersCountChanged, watchersAfterNewConnection);
        }
        else
        {
            await Clients.Client(Context.ConnectionId).SendAsync(SignalRConstants.ProductWatchersCountChanged, watchersAfterNewConnection);
        }
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        long productWithIdViewed = _connectionsManager.ProductAssociatedWithConnection(Context.ConnectionId);
        int watchersBeforeDisconnect = _connectionsManager.WatchingProductUsersCount(productWithIdViewed);
        _connectionsManager.RemoveConnection(Context.ConnectionId);
        int watchersAfterDisconnect = _connectionsManager.WatchingProductUsersCount(productWithIdViewed);
        bool watchersAmountChanged = watchersAfterDisconnect != watchersBeforeDisconnect;

        if (watchersAmountChanged)
        {
            IEnumerable<string> connectionsWatchingProduct = _connectionsManager.ConnectionsAssociatedWithProduct(productWithIdViewed);
            await Clients.Clients(connectionsWatchingProduct).SendAsync(SignalRConstants.ProductWatchersCountChanged, watchersAfterDisconnect);
        }
        await base.OnDisconnectedAsync(exception);
    }
}