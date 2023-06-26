using System.Text.Json.Serialization;
using Web.Extensions;
using Web.Hubs;
using Web.Middlewares;
using Web.SignalR;

namespace Web;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddControllersWithViews().AddJsonOptions(options =>
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
        services.AddHttpContextAccessor();
        services.AddAndConfigureAuthentication(Configuration);
        services.AddAndConfigureAuthorization();
        services.AddCustomDbContext(Configuration);
        services.AddMediatRServices();
        services.AddSignalRWithRelatedServices();
        services.AddCustomRepositories();
        services.AddCustomServices();
        services.AddScoped<GlobalExceptionHandlingMiddleware>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseDeveloperExceptionPage();
        }
        
        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHub<RealTimeProductInfoHub>(SignalRConstants.RealTimeProductInfoHubRoute);
        });
    }
}