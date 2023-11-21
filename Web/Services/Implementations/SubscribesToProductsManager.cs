using Redis.OM;
using Redis.OM.Contracts;
using Redis.OM.Modeling;
using Redis.OM.Searching;
using Web.Services.Interfaces;

namespace Web.Services.Implementations;

public class SubscribesToProductsManager(IRedisConnectionProvider redisConnection) : ISubscribesToProductsManager
{
    private readonly IRedisCollection<Subscribe> _subscriptions = redisConnection.RedisCollection<Subscribe>();

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
public class Subscribe(long userId, long productId, string connectionId)
{
    [Indexed]
    [RedisIdField]
    public string ConnectionId { get; } = connectionId;

    [Indexed]
    public long UserId { get; } = userId;

    [Indexed]
    public long ProductId { get; } = productId;
}