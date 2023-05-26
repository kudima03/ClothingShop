using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using DomainServices.Behaviors;
using FluentValidation;
using Infrastructure.Data;
using Infrastructure.EntityRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.Json.Serialization;

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
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
        services.AddValidatorsFromAssembly(typeof(ValidationBehaviour<,>).Assembly);
        AddCustomDbContext(Configuration, services);
        AddCustomRepositories(services);
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(typeof(ValidationBehaviour<,>).Assembly);
        });
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
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

        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }

    private static void AddCustomDbContext(IConfiguration configuration, IServiceCollection services)
    {
        services.AddEntityFrameworkNpgsql()
            .AddDbContext<DbContext, ShopContext>(options =>
            {
                options.UseNpgsql(configuration["ConnectionStrings:ShopConnection"],
                    sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(ShopContext).GetTypeInfo().Assembly.GetName().Name);
                        sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(3), null);
                    });
            });
    }

    private static void AddCustomRepositories(IServiceCollection services)
    {
        services.AddScoped<IRepository<Brand>, EntityFrameworkRepository<Brand>>();
        services.AddScoped<IReadOnlyRepository<Brand>, EntityFrameworkReadOnlyRepository<Brand>>();

        services.AddScoped<IRepository<Section>, EntityFrameworkRepository<Section>>();
        services.AddScoped<IReadOnlyRepository<Section>, EntityFrameworkReadOnlyRepository<Section>>();

        services.AddScoped<IRepository<Category>, EntityFrameworkRepository<Category>>();
        services.AddScoped<IReadOnlyRepository<Category>, EntityFrameworkReadOnlyRepository<Category>>();
    }
}