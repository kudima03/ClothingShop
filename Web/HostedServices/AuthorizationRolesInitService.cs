using Infrastructure.Data;
using Infrastructure.Identity.Entity;
using Infrastructure.Identity.IdentityContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Web.HostedServices;

public class AuthorizationRolesInitService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public AuthorizationRolesInitService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using AsyncServiceScope scope = _serviceProvider.CreateAsyncScope();

        UserManager<User> userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

        RoleManager<IdentityRole<long>>? roleManager = scope.ServiceProvider.GetRequiredService
                                                               (typeof(RoleManager<IdentityRole<long>>))
                                                           as RoleManager<IdentityRole<long>>;

        IdentityContext identityContext = scope.ServiceProvider.GetRequiredService<IdentityContext>();
        await identityContext.Database.MigrateAsync(cancellationToken);
        ShopContext shopContext = scope.ServiceProvider.GetRequiredService<ShopContext>();
        await IdentityContextSeed.SeedAsync(identityContext, shopContext, userManager, roleManager);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}