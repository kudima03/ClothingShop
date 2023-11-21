using Infrastructure.Identity.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Web.Services.Interfaces;
using Web.SignalR;

namespace Web.Hubs;

[Authorize(Policy = PolicyName.Customer)]
public class RealTimeProductInfoHub(ISubscribesToProductsManager subscribesManager) : Hub
{
    public async Task SubscribeForProductInfoChanges(long productId)
    {
        int watchersBeforeNewConnection = await subscribesManager.GetSubscribedToProductUsersCountAsync(productId);
        long userId = long.Parse(Context.UserIdentifier!);
        await subscribesManager.AddSubscribeAsync(userId, productId, Context.ConnectionId);
        int watchersAfterNewConnection = await subscribesManager.GetSubscribedToProductUsersCountAsync(productId);
        bool productWatchersAmountChanged = watchersAfterNewConnection != watchersBeforeNewConnection;

        if (productWatchersAmountChanged)
        {
            IEnumerable<string> connectionsWatchingProduct =
                await subscribesManager.GetConnectionsSubscribedToProductAsync(productId);

            Clients.Clients(connectionsWatchingProduct)
                   .SendAsync(SignalRConstants.ProductWatchersCountChanged, watchersAfterNewConnection);
        }

        Clients.Client(Context.ConnectionId).SendAsync(SignalRConstants.ProductWatchersCountChanged, watchersAfterNewConnection);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        long productWithIdViewed = await subscribesManager.GetProductIdConnectionSubscribedToAsync(Context.ConnectionId);
        int watchersBeforeDisconnect = await subscribesManager.GetSubscribedToProductUsersCountAsync(productWithIdViewed);
        await subscribesManager.RemoveSubscribeAsync(Context.ConnectionId);
        int watchersAfterDisconnect = await subscribesManager.GetSubscribedToProductUsersCountAsync(productWithIdViewed);
        bool watchersAmountChanged = watchersAfterDisconnect != watchersBeforeDisconnect;

        if (watchersAmountChanged)
        {
            IEnumerable<string> connectionsWatchingProduct = await subscribesManager.GetConnectionsSubscribedToProductAsync
                                                                 (productWithIdViewed);

            await Clients.Clients(connectionsWatchingProduct)
                         .SendAsync(SignalRConstants.ProductWatchersCountChanged, watchersAfterDisconnect);
        }

        await base.OnDisconnectedAsync(exception);
    }
}