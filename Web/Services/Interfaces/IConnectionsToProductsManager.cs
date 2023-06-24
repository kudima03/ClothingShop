namespace Web.Services.Interfaces;

public interface IConnectionsToProductsManager
{
    public void AddConnection(long userId, long productId, string connectionId);

    public void RemoveConnection(string connectionId);

    public IEnumerable<string> ConnectionsAssociatedWithProduct(long productId);

    public int WatchingProductUsersCount(long productId);

    public long ProductAssociatedWithConnection(string connectionId);
}