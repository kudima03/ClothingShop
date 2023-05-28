using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using DomainServices.Behaviors;
using DomainServices.Features.Brands.Queries;
using DomainServices.Features.Categories.Queries;
using DomainServices.Features.Sections.Queries;
using DomainServices.Features.Subcategories.Queries;
using DomainServices.Features.Templates.Commands.Create;
using DomainServices.Features.Templates.Commands.Delete;
using DomainServices.Features.Templates.Commands.Update;
using DomainServices.Features.Templates.Queries.CollectionQueries;
using DomainServices.Features.Templates.Queries.SingleEntityQueries;
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
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
        services.AddValidatorsFromAssembly(typeof(ValidationBehaviour<,>).Assembly);
        AddCustomDbContext(Configuration, services);
        AddCustomRepositories(services);
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(typeof(ValidationBehaviour<,>).Assembly);
        });
        AddMediatRServices(services);
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

    private static void AddMediatRServices(IServiceCollection services)
    {
        services.AddTransient(typeof(IRequestHandler<CreateCommand<Brand>, Brand>),
            typeof(CreateCommandHandler<Brand>));
        services.AddTransient(typeof(IRequestHandler<DeleteCommand<Brand>, Unit>), typeof(DeleteCommandHandler<Brand>));
        services.AddTransient(typeof(IRequestHandler<UpdateCommand<Brand>, Unit>), typeof(UpdateCommandHandler<Brand>));
        services.AddTransient(typeof(IRequestHandler<GetAllBrandsQuery, IEnumerable<Brand>>),
            typeof(EntityCollectionQueryHandler<Brand>));
        services.AddTransient(typeof(IRequestHandler<GetBrandByIdQuery, Brand>),
            typeof(SingleEntityQueryHandler<Brand>));

        services.AddTransient(typeof(IRequestHandler<CreateCommand<Section>, Section>),
            typeof(CreateCommandHandler<Section>));
        services.AddTransient(typeof(IRequestHandler<DeleteCommand<Section>, Unit>),
            typeof(DeleteCommandHandler<Section>));
        services.AddTransient(typeof(IRequestHandler<UpdateCommand<Section>, Unit>),
            typeof(UpdateCommandHandler<Section>));
        services.AddTransient(typeof(IRequestHandler<GetAllSectionsQuery, IEnumerable<Section>>),
            typeof(EntityCollectionQueryHandler<Section>));
        services.AddTransient(typeof(IRequestHandler<GetSectionByIdQuery, Section>),
            typeof(SingleEntityQueryHandler<Section>));

        services.AddTransient(typeof(IRequestHandler<CreateCommand<Category>, Category>),
            typeof(CreateCommandHandler<Category>));
        services.AddTransient(typeof(IRequestHandler<DeleteCommand<Category>, Unit>),
            typeof(DeleteCommandHandler<Category>));
        services.AddTransient(typeof(IRequestHandler<UpdateCommand<Category>, Unit>),
            typeof(UpdateCommandHandler<Category>));
        services.AddTransient(typeof(IRequestHandler<GetAllCategoriesQuery, IEnumerable<Category>>),
            typeof(EntityCollectionQueryHandler<Category>));
        services.AddTransient(typeof(IRequestHandler<GetCategoryByIdQuery, Category>),
            typeof(SingleEntityQueryHandler<Category>));

        services.AddTransient(typeof(IRequestHandler<CreateCommand<Subcategory>, Subcategory>),
            typeof(CreateCommandHandler<Subcategory>));
        services.AddTransient(typeof(IRequestHandler<DeleteCommand<Subcategory>, Unit>),
            typeof(DeleteCommandHandler<Subcategory>));
        services.AddTransient(typeof(IRequestHandler<UpdateCommand<Subcategory>, Unit>),
            typeof(UpdateCommandHandler<Subcategory>));
        services.AddTransient(typeof(IRequestHandler<GetAllSubcategoriesQuery, IEnumerable<Subcategory>>),
            typeof(EntityCollectionQueryHandler<Subcategory>));
        services.AddTransient(typeof(IRequestHandler<GetSubcategoryByIdQuery, Subcategory>),
            typeof(SingleEntityQueryHandler<Subcategory>));
    }
}