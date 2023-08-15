using Infrastructure.Identity.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Web.Services.Interfaces;
using Web.SignalR;

namespace Web.Hubs;

[Authorize(Policy = PolicyName.Customer)]
public class RealTimeProductInfoHub : Hub
{
    private readonly ISubscribesToProductsManager _subscribesManager;

    public RealTimeProductInfoHub(ISubscribesToProductsManager subscribesManager)
    {
        _subscribesManager = subscribesManager;
    }

    public async Task SubscribeForProductInfoChanges(long productId)
    {
        int watchersBeforeNewConnection = await _subscribesManager.GetSubscribedToProductUsersCountAsync(productId);
        long userId = long.Parse(Context.UserIdentifier!);
        await _subscribesManager.AddSubscribeAsync(userId, productId, Context.ConnectionId);
        int watchersAfterNewConnection = await _subscribesManager.GetSubscribedToProductUsersCountAsync(productId);
        bool productWatchersAmountChanged = watchersAfterNewConnection != watchersBeforeNewConnection;

        if (productWatchersAmountChanged)
        {
            IEnumerable<string> connectionsWatchingProduct =
                await _subscribesManager.GetConnectionsSubscribedToProductAsync(productId);

            Clients.Clients
                       (connectionsWatchingProduct)
                   .SendAsync(SignalRConstants.ProductWatchersCountChanged, watchersAfterNewConnection);
        }

        Clients.Client(Context.ConnectionId).SendAsync(SignalRConstants.ProductWatchersCountChanged, watchersAfterNewConnection);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        long productWithIdViewed = await _subscribesManager.GetProductIdConnectionSubscribedToAsync(Context.ConnectionId);
        int watchersBeforeDisconnect = await _subscribesManager.GetSubscribedToProductUsersCountAsync(productWithIdViewed);
        await _subscribesManager.RemoveSubscribeAsync(Context.ConnectionId);
        int watchersAfterDisconnect = await _subscribesManager.GetSubscribedToProductUsersCountAsync(productWithIdViewed);
        bool watchersAmountChanged = watchersAfterDisconnect != watchersBeforeDisconnect;

        if (watchersAmountChanged)
        {
            IEnumerable<string> connectionsWatchingProduct = await _subscribesManager.GetConnectionsSubscribedToProductAsync
                                                                 (productWithIdViewed);

            await Clients.Clients(connectionsWatchingProduct)
                         .SendAsync(SignalRConstants.ProductWatchersCountChanged, watchersAfterDisconnect);
        }

        await base.OnDisconnectedAsync(exception);
    }
}