using Redis.OM;
using Redis.OM.Contracts;
using Web.Services.Implementations;

namespace Web.HostedServices;

public class RedisInitService : IHostedService
{
    private readonly IRedisConnectionProvider _provider;

    public RedisInitService(IRedisConnectionProvider provider)
    {
        _provider = provider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _provider.Connection.DropIndexAsync(typeof(Subscribe));
        await _provider.Connection.CreateIndexAsync(typeof(Subscribe));
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        IList<Subscribe> itemsToDelete = await _provider.RedisCollection<Subscribe>().ToListAsync();
        await _provider.RedisCollection<Subscribe>().DeleteAsync(itemsToDelete);
    }
}