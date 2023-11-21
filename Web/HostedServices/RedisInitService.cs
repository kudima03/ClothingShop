using Redis.OM;
using Redis.OM.Contracts;
using Web.Services.Implementations;

namespace Web.HostedServices;

public class RedisInitService(IRedisConnectionProvider provider) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await provider.Connection.DropIndexAsync(typeof(Subscribe));
        await provider.Connection.CreateIndexAsync(typeof(Subscribe));
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        IList<Subscribe> itemsToDelete = await provider.RedisCollection<Subscribe>().ToListAsync();
        await provider.RedisCollection<Subscribe>().DeleteAsync(itemsToDelete);
    }
}