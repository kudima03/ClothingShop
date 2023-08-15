using Redis.OM;
using Redis.OM.Contracts;
using Redis.OM.Modeling;
using Redis.OM.Searching;
using Web.Services.Interfaces;

namespace Web.Services.Implementations;

public class SubscribesToProductsManager : ISubscribesToProductsManager
{
    private readonly IRedisCollection<Subscribe> _subscriptions;

    public SubscribesToProductsManager(IRedisConnectionProvider redisConnection)
    {
        _subscriptions = redisConnection.RedisCollection<Subscribe>();
    }

    public Task AddSubscribeAsync(long userId, long productId, string connectionId)
    {
        _subscriptions.InsertAsync(new Subscribe(userId, productId, connectionId));

        return Task.CompletedTask;
    }

    public async Task RemoveSubscribeAsync(string connectionId)
    {
        Subscribe subscription = await _subscriptions.FindByIdAsync(connectionId);
        _subscriptions.DeleteAsync(subscription);
    }

    public async Task<IEnumerable<string>> GetConnectionsSubscribedToProductAsync(long productId)
    {
        return await _subscriptions.Where(subscribe => subscribe.ProductId == productId)
                                   .Select(subscribe => subscribe.ConnectionId)
                                   .ToListAsync();
    }

    public Task<int> GetSubscribedToProductUsersCountAsync(long productId)
    {
        return Task.FromResult
            (_subscriptions.Where(subscribe => subscribe.ProductId == productId)
                           .DistinctBy(subscribe => subscribe.UserId)
                           .Count());
    }

    public async Task<long> GetProductIdConnectionSubscribedToAsync(string connectionId)
    {
        return (await _subscriptions.SingleAsync(subscribe => subscribe.ConnectionId == connectionId)).ProductId;
    }
}

[Document(StorageType = StorageType.Json)]
public class Subscribe
{
    public Subscribe(long userId, long productId, string connectionId)
    {
        UserId = userId;
        ProductId = productId;
        ConnectionId = connectionId;
    }

    [Indexed]
    [RedisIdField]
    public string ConnectionId { get; }

    [Indexed]
    public long UserId { get; }

    [Indexed]
    public long ProductId { get; }
}