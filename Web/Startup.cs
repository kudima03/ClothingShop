using DomainServices.Behaviors;
using FluentValidation;
using MediatR;
using System.Text.Json.Serialization;
using Web.Extensions;
using Web.Middlewares;

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
        services.AddControllers().AddJsonOptions(options =>
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
        services.AddValidatorsFromAssembly(typeof(ValidationBehaviour<,>).Assembly);
        services.AddCustomDbContext(Configuration);
        services.AddCustomRepositories();
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(typeof(ValidationBehaviour<,>).Assembly);
        });
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
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

        app.UseRouting();

        app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}