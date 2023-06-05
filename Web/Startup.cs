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
        AddCustomDbContext(Configuration, services);
        AddCustomRepositories(services);
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

        services.AddScoped<IRepository<Color>, EntityFrameworkRepository<Color>>();
        services.AddScoped<IReadOnlyRepository<Color>, EntityFrameworkReadOnlyRepository<Color>>();

        services.AddScoped<IRepository<CustomerInfo>, EntityFrameworkRepository<CustomerInfo>>();
        services.AddScoped<IReadOnlyRepository<CustomerInfo>, EntityFrameworkReadOnlyRepository<CustomerInfo>>();

        services.AddScoped<IRepository<ImageInfo>, EntityFrameworkRepository<ImageInfo>>();
        services.AddScoped<IReadOnlyRepository<ImageInfo>, EntityFrameworkReadOnlyRepository<ImageInfo>>();

        services.AddScoped<IRepository<Order>, EntityFrameworkRepository<Order>>();
        services.AddScoped<IReadOnlyRepository<Order>, EntityFrameworkReadOnlyRepository<Order>>();

        services.AddScoped<IRepository<OrderStatus>, EntityFrameworkRepository<OrderStatus>>();
        services.AddScoped<IReadOnlyRepository<OrderStatus>, EntityFrameworkReadOnlyRepository<OrderStatus>>();

        services.AddScoped<IRepository<Product>, EntityFrameworkRepository<Product>>();
        services.AddScoped<IReadOnlyRepository<Product>, EntityFrameworkReadOnlyRepository<Product>>();

        services.AddScoped<IRepository<ProductColor>, EntityFrameworkRepository<ProductColor>>();
        services.AddScoped<IReadOnlyRepository<ProductColor>, EntityFrameworkReadOnlyRepository<ProductColor>>();

        services.AddScoped<IRepository<ProductOption>, EntityFrameworkRepository<ProductOption>>();
        services.AddScoped<IReadOnlyRepository<ProductOption>, EntityFrameworkReadOnlyRepository<ProductOption>>();

        services.AddScoped<IRepository<Review>, EntityFrameworkRepository<Review>>();
        services.AddScoped<IReadOnlyRepository<Review>, EntityFrameworkReadOnlyRepository<Review>>();

        services.AddScoped<IRepository<Section>, EntityFrameworkRepository<Section>>();
        services.AddScoped<IReadOnlyRepository<Section>, EntityFrameworkReadOnlyRepository<Section>>();

        services.AddScoped<IRepository<Subcategory>, EntityFrameworkRepository<Subcategory>>();
        services.AddScoped<IReadOnlyRepository<Subcategory>, EntityFrameworkReadOnlyRepository<Subcategory>>();

        services.AddScoped<IRepository<User>, EntityFrameworkRepository<User>>();
        services.AddScoped<IReadOnlyRepository<User>, EntityFrameworkReadOnlyRepository<User>>();

        services.AddScoped<IRepository<UserType>, EntityFrameworkRepository<UserType>>();
        services.AddScoped<IReadOnlyRepository<UserType>, EntityFrameworkReadOnlyRepository<UserType>>();
    }
}