using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Web.HostedServices;

public class ShopContextSeedService(IServiceProvider serviceProvider) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using AsyncServiceScope scope = serviceProvider.CreateAsyncScope();

        ShopContext shopContext = scope.ServiceProvider.GetRequiredService<ShopContext>();

        if (!shopContext.Database.IsInMemory())
        {
            await shopContext.Database.MigrateAsync(cancellationToken);
        }

        await ShopContextSeed.SeedAsync(shopContext);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}