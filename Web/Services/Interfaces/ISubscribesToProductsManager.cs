namespace Web.Services.Interfaces;

public interface ISubscribesToProductsManager
{
    void AddSubscribe(long userId, long productId, string connectionId);

    void RemoveSubscribe(string connectionId);

    IEnumerable<string> ConnectionsSubscribedToProduct(long productId);

    int SubscribedToProductUsersCount(long productId);

    long ProductConnectionSubscribedTo(string connectionId);
}