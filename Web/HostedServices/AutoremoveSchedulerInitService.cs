using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.Data.Autoremove;

namespace Web.HostedServices;

public class AutoremoveSchedulerInitService(IServiceProvider serviceProvider) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using AsyncServiceScope scope = serviceProvider.CreateAsyncScope();

        IReadOnlyRepository<ShoppingCartItem> repository =
            scope.ServiceProvider.GetRequiredService<IReadOnlyRepository<ShoppingCartItem>>();

        ShoppingCartItemsAutoremoveScheduler service =
            scope.ServiceProvider.GetRequiredService<ShoppingCartItemsAutoremoveScheduler>();

        IList<ShoppingCartItem> shoppingCartItems = await repository.GetAllAsync(cancellationToken);

        foreach (ShoppingCartItem? shoppingCartItem in shoppingCartItems)
        {
            await service.AddToSchedule(shoppingCartItem, cancellationToken);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}