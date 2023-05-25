using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using DomainServices.Services.Implementations;
using DomainServices.Services.Interfaces;
using FluentValidation;
using Infrastructure.Data;
using Infrastructure.EntityRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Web.Behaviors;

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
        services.AddControllers();
        services.AddValidatorsFromAssembly(typeof(Startup).Assembly);
        AddCustomDbContext(Configuration, services);
        services.AddScoped<IRepository<Brand>, EntityFrameworkRepository<Brand>>();
        services.AddScoped<IReadOnlyRepository<Brand>, EntityFrameworkReadOnlyRepository<Brand>>();
        services.AddScoped<IBrandsService, BrandsService>();
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(typeof(Startup).Assembly);
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
}