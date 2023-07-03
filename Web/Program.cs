using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Autoremove;
using Infrastructure.Identity.Entity;
using Infrastructure.Identity.IdentityContext;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Identity;

namespace Web;

internal class Program
{
    public static async Task Main(string[] args)
    {
        IWebHost webHost = CreateHostBuilder(args).Build();

        using (IServiceScope scope = webHost.Services.CreateScope())
        {
            IServiceProvider scopedProvider = scope.ServiceProvider;

            ShopContext shopContext = scopedProvider.GetRequiredService<ShopContext>();
            await ShopContextSeed.SeedAsync(shopContext);
        }

        using (IServiceScope scope = webHost.Services.CreateScope())
        {
            IServiceProvider scopedProvider = scope.ServiceProvider;

            UserManager<User> userManager = scopedProvider.GetRequiredService<UserManager<User>>();

            RoleManager<IdentityRole<long>>? roleManager = scopedProvider.GetRequiredService
                                                                   (typeof(RoleManager<IdentityRole<long>>))
                                                               as RoleManager<IdentityRole<long>>;

            IdentityContext identityContext = scopedProvider.GetRequiredService<IdentityContext>();
            ShopContext shopContext = scopedProvider.GetRequiredService<ShopContext>();
            await IdentityContextSeed.SeedAsync(identityContext, shopContext, userManager, roleManager);
        }

        using (IServiceScope scope = webHost.Services.CreateScope())
        {
            IServiceProvider scopedProvider = scope.ServiceProvider;

            IReadOnlyRepository<ShoppingCartItem> repository =
                scopedProvider.GetRequiredService<IReadOnlyRepository<ShoppingCartItem>>();

            ShoppingCartItemsAutoremoveScheduler service =
                scopedProvider.GetRequiredService<ShoppingCartItemsAutoremoveScheduler>();

            List<ShoppingCartItem> items = repository.GetAll().ToList();
            items.ForEach(x => service.AddToSchedule(x).Wait());
        }

        await webHost.RunAsync();
    }

    public static IWebHostBuilder CreateHostBuilder(string[] args)
    {
        return WebHost.CreateDefaultBuilder(args)
                      .UseStartup<Startup>();
    }
}