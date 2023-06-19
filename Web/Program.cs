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

            UserManager<User> userManager = scopedProvider.GetRequiredService<UserManager<User>>();
            RoleManager<IdentityRole<long>>? roleManager = scopedProvider.GetRequiredService(typeof(RoleManager<IdentityRole<long>>))
                as RoleManager<IdentityRole<long>>;
            IdentityContext identityContext = scopedProvider.GetRequiredService<IdentityContext>();
            await IdentityContextSeed.SeedAsync(identityContext, userManager, roleManager);
        }

        await webHost.RunAsync();
    }

    public static IWebHostBuilder CreateHostBuilder(string[] args)
    {
        return WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>();
    }
}