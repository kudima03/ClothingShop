namespace Web.Services.Interfaces;

public interface ISubscribesToProductsManager
{
    Task AddSubscribeAsync(long userId, long productId, string connectionId);

    Task RemoveSubscribeAsync(string connectionId);

    Task<IEnumerable<string>> GetConnectionsSubscribedToProductAsync(long productId);

    Task<int> GetSubscribedToProductUsersCountAsync(long productId);

    Task<long> GetProductIdConnectionSubscribedToAsync(string connectionId);
}