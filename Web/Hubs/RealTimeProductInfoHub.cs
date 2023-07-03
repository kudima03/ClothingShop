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

    public Task SubscribeForProductInfoChanges(long productId)
    {
        int watchersBeforeNewConnection = _subscribesManager.SubscribedToProductUsersCount(productId);
        long userId = long.Parse(Context.UserIdentifier!);
        _subscribesManager.AddSubscribe(userId, productId, Context.ConnectionId);
        int watchersAfterNewConnection = _subscribesManager.SubscribedToProductUsersCount(productId);
        bool productWatchersAmountChanged = watchersAfterNewConnection != watchersBeforeNewConnection;

        if (productWatchersAmountChanged)
        {
            IEnumerable<string> connectionsWatchingProduct = _subscribesManager.ConnectionsSubscribedToProduct(productId);

            return Clients.Clients
                              (connectionsWatchingProduct)
                          .SendAsync(SignalRConstants.ProductWatchersCountChanged, watchersAfterNewConnection);
        }

        return Clients.Client
                          (Context.ConnectionId)
                      .SendAsync(SignalRConstants.ProductWatchersCountChanged, watchersAfterNewConnection);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        long productWithIdViewed = _subscribesManager.ProductConnectionSubscribedTo(Context.ConnectionId);
        int watchersBeforeDisconnect = _subscribesManager.SubscribedToProductUsersCount(productWithIdViewed);
        _subscribesManager.RemoveSubscribe(Context.ConnectionId);
        int watchersAfterDisconnect = _subscribesManager.SubscribedToProductUsersCount(productWithIdViewed);
        bool watchersAmountChanged = watchersAfterDisconnect != watchersBeforeDisconnect;

        if (watchersAmountChanged)
        {
            IEnumerable<string> connectionsWatchingProduct = _subscribesManager.ConnectionsSubscribedToProduct
                (productWithIdViewed);

            await Clients.Clients
                             (connectionsWatchingProduct)
                         .SendAsync(SignalRConstants.ProductWatchersCountChanged, watchersAfterDisconnect);
        }

        await base.OnDisconnectedAsync(exception);
    }
}