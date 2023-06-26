using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using DomainServices.Behaviors;
using DomainServices.Services.OrdersService;
using FluentValidation;
using Infrastructure.Data;
using Infrastructure.Data.Triggers;
using Infrastructure.EntityRepository;
using Infrastructure.Identity.Constants;
using Infrastructure.Identity.Entity;
using Infrastructure.Identity.IdentityContext;
using Infrastructure.Identity.Interfaces;
using Infrastructure.Identity.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using Web.Services.Implementations;
using Web.Services.Interfaces;
using Web.SignalR;

namespace Web.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddCustomRepositories(this IServiceCollection services)
    {
        services.AddScoped<IRepository<Brand>, EntityFrameworkRepository<Brand>>();
        services.AddScoped<IReadOnlyRepository<Brand>, EntityFrameworkReadOnlyRepository<Brand>>();

        services.AddScoped<IRepository<Section>, EntityFrameworkRepository<Section>>();
        services.AddScoped<IReadOnlyRepository<Section>, EntityFrameworkReadOnlyRepository<Section>>();

        services.AddScoped<IRepository<Category>, EntityFrameworkRepository<Category>>();
        services.AddScoped<IReadOnlyRepository<Category>, EntityFrameworkReadOnlyRepository<Category>>();

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
    }

    public static void AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
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

                options.UseTriggers(triggerOptions =>
                {
                    triggerOptions.AddTrigger<ProductOptionAfterUpdateTrigger>();
                });
            });
    }

    public static void AddMediatRServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(ValidationBehaviour<,>).Assembly);
        services.AddValidatorsFromAssembly(typeof(IdentityContext).Assembly);
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(typeof(ValidationBehaviour<,>).Assembly);
            options.RegisterServicesFromAssembly(typeof(IdentityContext).Assembly);
            options.RegisterServicesFromAssembly(typeof(Program).Assembly);
        });
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
    }

    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped<IOrdersService, OrdersService>();
    }

    public static void AddAndConfigureAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(PolicyName.Administrator, builder =>
            {
                builder.RequireClaim(ClaimTypes.Role, RoleName.Administrator);
                builder.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
            });

            options.AddPolicy(PolicyName.Customer, builder =>
            {
                builder.RequireAssertion(x => x.User.HasClaim(ClaimTypes.Role, RoleName.Customer) ||
                                              x.User.HasClaim(ClaimTypes.Role, RoleName.Administrator));
                builder.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
            });
        });
    }

    public static void AddAndConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication()
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidAudience = JwtSettings.Audience,
                        ValidIssuer = JwtSettings.Issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.SecretKey)),
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies[JwtConstants.TokenType];
                            return Task.CompletedTask;
                        }
                    };
                });

        services.AddEntityFrameworkNpgsql()
                .AddDbContext<IdentityContext>(options =>
                {
                    options.UseNpgsql(configuration["ConnectionStrings:IdentityConnection"],
                                      sqlOptions =>
                                      {
                                          sqlOptions.MigrationsAssembly(typeof(IdentityContext).GetTypeInfo().Assembly.GetName().Name);
                                          sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(3), null);
                                      });
                });

        services.AddIdentity<User, IdentityRole<long>>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

        services.AddScoped<IAuthorizationService, AuthorizationService>();

        services.AddScoped<ITokenClaimsService, IdentityTokenClaimsService>();
    }

    public static void AddSignalRWithRelatedServices(this IServiceCollection services)
    {
        services.AddSignalR();
        services.AddSingleton<IUserIdProvider, SignalRUserIdProvider>();
        services.AddSingleton<ISubscribesToProductsManager, SubscribesToProductsManager>();
    }
}