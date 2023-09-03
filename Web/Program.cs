using Microsoft.AspNetCore;

namespace Web;

internal class Program
{
    public static async Task Main(string[] args)
    {
        await CreateHostBuilder(args).Build().RunAsync();
    }

    public static IWebHostBuilder CreateHostBuilder(string[] args)
    {
        return WebHost.CreateDefaultBuilder(args)
                      .ConfigureAppConfiguration
                          (builder =>
                          {
                              builder.AddEnvironmentVariables();
                          })
                      .UseStartup<Startup>();
    }
}