using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Web.HostedServices;

public class ShopContextSeedService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public ShopContextSeedService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using AsyncServiceScope scope = _serviceProvider.CreateAsyncScope();

        ShopContext shopContext = scope.ServiceProvider.GetRequiredService<ShopContext>();
        await shopContext.Database.MigrateAsync(cancellationToken);
        await ShopContextSeed.SeedAsync(shopContext);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}