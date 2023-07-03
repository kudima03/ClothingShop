using Web.Services.Interfaces;

namespace Web.Services.Implementations;

public class SubscribesToProductsManager : ISubscribesToProductsManager
{
    private readonly List<Subscribe> _subscribes = new List<Subscribe>();

    public void AddSubscribe(long userId, long productId, string connectionId)
    {
        _subscribes.Add(new Subscribe(userId, productId, connectionId));
    }

    public void RemoveSubscribe(string connectionId)
    {
        Subscribe subscribe =
            _subscribes.Single(subscribe => subscribe.ConnectionId == connectionId);

        _subscribes.Remove(subscribe);
    }

    public IEnumerable<string> ConnectionsSubscribedToProduct(long productId)
    {
        return _subscribes.Where(subscribe => subscribe.ProductId == productId)
                          .Select(subscribe => subscribe.ConnectionId);
    }

    public int SubscribedToProductUsersCount(long productId)
    {
        return _subscribes.Where(subscribe => subscribe.ProductId == productId)
                          .DistinctBy(subscribe => subscribe.UserId)
                          .Count();
    }

    public long ProductConnectionSubscribedTo(string connectionId)
    {
        return _subscribes.Single(subscribe => subscribe.ConnectionId == connectionId).ProductId;
    }
}

public class Subscribe
{
    public Subscribe(long userId, long productId, string connectionId)
    {
        UserId = userId;
        ProductId = productId;
        ConnectionId = connectionId;
    }

    public long UserId { get; }

    public long ProductId { get; }

    public string ConnectionId { get; }
}