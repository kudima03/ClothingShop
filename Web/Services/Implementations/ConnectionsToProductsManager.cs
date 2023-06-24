using Web.Services.Interfaces;

namespace Web.Services.Implementations;

public class ConnectionsToProductsManager : IConnectionsToProductsManager
{
    private readonly List<ConnectionInfo> _usersConnections = new List<ConnectionInfo>();

    public void AddConnection(long userId, long productId, string connectionId)
    {
        _usersConnections.Add(new ConnectionInfo(userId, productId, connectionId));
    }

    public void RemoveConnection(string connectionId)
    {
        ConnectionInfo connectionToRemove = _usersConnections.Single(x => x.ConnectionId == connectionId);
        _usersConnections.Remove(connectionToRemove);
    }

    public IEnumerable<string> ConnectionsAssociatedWithProduct(long productId)
    {
        return _usersConnections.Where(x => x.ProductId == productId).Select(x => x.ConnectionId);
    }

    public int WatchingProductUsersCount(long productId)
    {
        return _usersConnections.Where(x => x.ProductId == productId).DistinctBy(x => x.UserId).Count();
    }

    public long ProductAssociatedWithConnection(string connectionId)
    {
        return _usersConnections.Single(x => x.ConnectionId == connectionId).ProductId;
    }
}

public class ConnectionInfo
{
    public ConnectionInfo(long userId, long productId, string connectionId)
    {
        UserId = userId;
        ProductId = productId;
        ConnectionId = connectionId;
    }
    public long UserId { get; init; }
    public long ProductId { get; init; }
    public string ConnectionId { get; init; }
}