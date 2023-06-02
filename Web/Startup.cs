using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using DomainServices.Behaviors;
using DomainServices.Features.Brands.Queries;
using DomainServices.Features.Categories.Commands.AssociateSubcategory;
using DomainServices.Features.Categories.Commands.DeassociateSubcategory;
using DomainServices.Features.Categories.Queries;
using DomainServices.Features.Colors.Queries;
using DomainServices.Features.Customers.BusinessRulesValidators;
using DomainServices.Features.Customers.Queries;
using DomainServices.Features.ImagesInfos.BusinessRulesValidators;
using DomainServices.Features.ImagesInfos.Queries;
using DomainServices.Features.Orders.BusinessRulesValidators;
using DomainServices.Features.Orders.Queries;
using DomainServices.Features.ProductColors.BusinessRulesValidators;
using DomainServices.Features.ProductColors.Queries;
using DomainServices.Features.Products.BusinessRulesValidators;
using DomainServices.Features.Products.Queries;
using DomainServices.Features.ProductsOptions.BusinessRulesValidators;
using DomainServices.Features.ProductsOptions.Queries;
using DomainServices.Features.Reviews.BusinessRulesValidators;
using DomainServices.Features.Reviews.Queries;
using DomainServices.Features.Sections.Commands.AssociateCategory;
using DomainServices.Features.Sections.Commands.DeassociateCategory;
using DomainServices.Features.Sections.Queries;
using DomainServices.Features.Subcategories.Queries;
using DomainServices.Features.Templates.BusinessRulesValidators;
using DomainServices.Features.Templates.Commands.Create;
using DomainServices.Features.Templates.Commands.Delete;
using DomainServices.Features.Templates.Commands.Update;
using DomainServices.Features.Templates.Queries.CollectionQueries;
using DomainServices.Features.Templates.Queries.SingleEntityQueries;
using DomainServices.Features.Users.BusinessRulesValidators;
using DomainServices.Features.Users.Queries;
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
        AddMediatRServices(services);
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

    private static void AddMediatRServices(IServiceCollection services)
    {
        services.AddTransient(typeof(IRequestHandler<CreateCommand<Brand>, Brand>),
            typeof(CreateCommandHandler<Brand>));
        services.AddTransient(typeof(IRequestHandler<DeleteCommand<Brand>, Unit>),
            typeof(DeleteCommandHandler<Brand>));
        services.AddTransient(typeof(IRequestHandler<UpdateCommand<Brand>, Unit>),
            typeof(UpdateCommandHandler<Brand>));
        services.AddTransient(typeof(IRequestHandler<GetAllBrandsQuery, IEnumerable<Brand>>),
            typeof(EntityCollectionQueryHandler<Brand>));
        services.AddTransient(typeof(IRequestHandler<GetBrandByIdQuery, Brand>),
            typeof(SingleEntityQueryHandler<Brand>));


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
        services.AddTransient(typeof(IRequestHandler<AssociateSubcategoryWithCategoryCommand, Unit>),
            typeof(AssociateSubcategoryWithCategoryCommandHandler));
        services.AddTransient(typeof(IRequestHandler<DeassociateSubcategoryWithCategoryCommand, Unit>),
            typeof(DeassociateSubcategoryWithCategoryCommand));


        services.AddTransient(typeof(IRequestHandler<CreateCommand<Color>, Color>),
            typeof(CreateCommandHandler<Color>));
        services.AddTransient(typeof(IRequestHandler<DeleteCommand<Color>, Unit>),
            typeof(DeleteCommandHandler<Color>));
        services.AddTransient(typeof(IRequestHandler<UpdateCommand<Color>, Unit>),
            typeof(UpdateCommandHandler<Color>));
        services.AddTransient(typeof(IRequestHandler<GetAllColorsQuery, IEnumerable<Color>>),
            typeof(EntityCollectionQueryHandler<Color>));
        services.AddTransient(typeof(IRequestHandler<GetColorByIdQuery, Color>),
            typeof(SingleEntityQueryHandler<Color>));


        services.AddTransient(typeof(IRequestHandler<CreateCommand<CustomerInfo>, CustomerInfo>),
            typeof(CreateCommandHandler<CustomerInfo>));
        services.AddTransient(typeof(IRequestHandler<DeleteCommand<CustomerInfo>, Unit>),
            typeof(DeleteCommandHandler<CustomerInfo>));
        services.AddTransient(typeof(IRequestHandler<UpdateCommand<CustomerInfo>, Unit>),
            typeof(UpdateCommandHandler<CustomerInfo>));
        services.AddTransient(typeof(IRequestHandler<GetAllCustomersQuery, IEnumerable<CustomerInfo>>),
            typeof(EntityCollectionQueryHandler<CustomerInfo>));
        services.AddTransient(typeof(IRequestHandler<GetCustomerByIdQuery, CustomerInfo>),
            typeof(SingleEntityQueryHandler<CustomerInfo>));
        services.AddTransient<IBusinessRulesValidator<CreateCommand<CustomerInfo>>,
            CreateCustomerInfoBusinessRulesValidator>();
        services.AddTransient<IBusinessRulesValidator<UpdateCommand<CustomerInfo>>,
            UpdateCustomerInfoBusinessRulesValidator>();


        services.AddTransient(typeof(IRequestHandler<CreateCommand<ImageInfo>, ImageInfo>),
            typeof(CreateCommandHandler<ImageInfo>));
        services.AddTransient(typeof(IRequestHandler<DeleteCommand<ImageInfo>, Unit>),
            typeof(DeleteCommandHandler<ImageInfo>));
        services.AddTransient(typeof(IRequestHandler<UpdateCommand<ImageInfo>, Unit>),
            typeof(UpdateCommandHandler<ImageInfo>));
        services.AddTransient(typeof(IRequestHandler<GetAllImagesInfosQuery, IEnumerable<ImageInfo>>),
            typeof(EntityCollectionQueryHandler<ImageInfo>));
        services.AddTransient(typeof(IRequestHandler<GetImageInfoByIdQuery, ImageInfo>),
            typeof(SingleEntityQueryHandler<ImageInfo>));
        services.AddTransient<IBusinessRulesValidator<CreateCommand<ImageInfo>>,
            CreateImageInfoBusinessRulesValidator>();
        services.AddTransient<IBusinessRulesValidator<UpdateCommand<ImageInfo>>,
            UpdateImageInfoBusinessRulesValidator>();


        services.AddTransient(typeof(IRequestHandler<CreateCommand<Order>, Order>),
            typeof(CreateCommandHandler<Order>));
        services.AddTransient(typeof(IRequestHandler<DeleteCommand<Order>, Unit>),
            typeof(DeleteCommandHandler<Order>));
        services.AddTransient(typeof(IRequestHandler<UpdateCommand<Order>, Unit>),
            typeof(UpdateCommandHandler<Order>));
        services.AddTransient(typeof(IRequestHandler<GetAllOrdersQuery, IEnumerable<Order>>),
            typeof(EntityCollectionQueryHandler<Order>));
        services.AddTransient(typeof(IRequestHandler<GetOrderByIdQuery, Order>),
            typeof(SingleEntityQueryHandler<Order>));
        services.AddTransient<IBusinessRulesValidator<CreateCommand<Order>>,
            CreateOrderBusinessRulesValidator>();
        services.AddTransient<IBusinessRulesValidator<UpdateCommand<Order>>,
            UpdateOrderBusinessRulesValidator>();


        services.AddTransient(typeof(IRequestHandler<CreateCommand<ProductColor>, ProductColor>),
            typeof(CreateCommandHandler<ProductColor>));
        services.AddTransient(typeof(IRequestHandler<DeleteCommand<ProductColor>, Unit>),
            typeof(DeleteCommandHandler<ProductColor>));
        services.AddTransient(typeof(IRequestHandler<UpdateCommand<ProductColor>, Unit>),
            typeof(UpdateCommandHandler<ProductColor>));
        services.AddTransient(typeof(IRequestHandler<GetAllProductColorsQuery, IEnumerable<ProductColor>>),
            typeof(EntityCollectionQueryHandler<ProductColor>));
        services.AddTransient(typeof(IRequestHandler<GetProductColorByIdQuery, ProductColor>),
            typeof(SingleEntityQueryHandler<ProductColor>));
        services.AddTransient<IBusinessRulesValidator<CreateCommand<ProductColor>>,
            CreateProductColorBusinessRulesValidator>();
        services.AddTransient<IBusinessRulesValidator<UpdateCommand<ProductColor>>,
            UpdateProductColorBusinessRulesValidator>();


        services.AddTransient(typeof(IRequestHandler<CreateCommand<Product>, Product>),
            typeof(CreateCommandHandler<Product>));
        services.AddTransient(typeof(IRequestHandler<DeleteCommand<Product>, Unit>),
            typeof(DeleteCommandHandler<Product>));
        services.AddTransient(typeof(IRequestHandler<UpdateCommand<Product>, Unit>),
            typeof(UpdateCommandHandler<Product>));
        services.AddTransient(typeof(IRequestHandler<GetAllProductsQuery, IEnumerable<Product>>),
            typeof(EntityCollectionQueryHandler<Product>));
        services.AddTransient(typeof(IRequestHandler<GetProductByIdQuery, Product>),
            typeof(SingleEntityQueryHandler<Product>));
        services.AddTransient(typeof(IRequestHandler<GetProductsByBrandIdQuery, IEnumerable<Product>>),
            typeof(EntityCollectionQueryHandler<Product>));
        services.AddTransient(typeof(IRequestHandler<GetProductsBySubcategoryIdQuery, IEnumerable<Product>>),
            typeof(EntityCollectionQueryHandler<Product>));
        services.AddTransient<IBusinessRulesValidator<CreateCommand<Product>>,
            CreateProductBusinessRulesValidator>();
        services.AddTransient<IBusinessRulesValidator<UpdateCommand<Product>>,
            UpdateProductBusinessRulesValidator>();


        services.AddTransient(typeof(IRequestHandler<CreateCommand<ProductOption>, ProductOption>),
            typeof(CreateCommandHandler<ProductOption>));
        services.AddTransient(typeof(IRequestHandler<DeleteCommand<ProductOption>, Unit>),
            typeof(DeleteCommandHandler<Product>));
        services.AddTransient(typeof(IRequestHandler<UpdateCommand<ProductOption>, Unit>),
            typeof(UpdateCommandHandler<ProductOption>));
        services.AddTransient(typeof(IRequestHandler<GetAllProductsOptionsQuery, IEnumerable<ProductOption>>),
            typeof(EntityCollectionQueryHandler<ProductOption>));
        services.AddTransient(typeof(IRequestHandler<GetProductOptionByIdQuery, ProductOption>),
            typeof(SingleEntityQueryHandler<ProductOption>));
        services.AddTransient<IBusinessRulesValidator<CreateCommand<ProductOption>>,
            CreateProductOptionBusinessRulesValidator>();
        services.AddTransient<IBusinessRulesValidator<UpdateCommand<ProductOption>>,
            UpdateProductOptionBusinessRulesValidator>();


        services.AddTransient(typeof(IRequestHandler<CreateCommand<Review>, Review>),
            typeof(CreateCommandHandler<Review>));
        services.AddTransient(typeof(IRequestHandler<DeleteCommand<Review>, Unit>),
            typeof(DeleteCommandHandler<Review>));
        services.AddTransient(typeof(IRequestHandler<UpdateCommand<Review>, Unit>),
            typeof(UpdateCommandHandler<Review>));
        services.AddTransient(typeof(IRequestHandler<GetAllReviewsQuery, IEnumerable<Review>>),
            typeof(EntityCollectionQueryHandler<Review>));
        services.AddTransient(typeof(IRequestHandler<GetReviewByIdQuery, Review>),
            typeof(SingleEntityQueryHandler<Review>));
        services.AddTransient<IBusinessRulesValidator<CreateCommand<Review>>,
            CreateReviewBusinessRulesValidator>();
        services.AddTransient<IBusinessRulesValidator<UpdateCommand<Review>>,
            UpdateReviewBusinessRulesValidator>();


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
        services.AddTransient(typeof(IRequestHandler<AssociateCategoryWithSectionCommand, Unit>),
            typeof(AssociateCategoryWithSectionCommandHandler));
        services.AddTransient(typeof(IRequestHandler<DeassociateCategoryWithSectionCommand, Unit>),
            typeof(DeassociateCategoryWithSectionCommandHandler));


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


        services.AddTransient(typeof(IRequestHandler<CreateCommand<User>, User>),
            typeof(CreateCommandHandler<User>));
        services.AddTransient(typeof(IRequestHandler<DeleteCommand<User>, Unit>),
            typeof(DeleteCommandHandler<User>));
        services.AddTransient(typeof(IRequestHandler<UpdateCommand<User>, Unit>),
            typeof(UpdateCommandHandler<User>));
        services.AddTransient(typeof(IRequestHandler<GetAllUsersQuery, IEnumerable<User>>),
            typeof(EntityCollectionQueryHandler<User>));
        services.AddTransient(typeof(IRequestHandler<GetUserByIdQuery, User>),
            typeof(SingleEntityQueryHandler<User>));
        services.AddTransient<IBusinessRulesValidator<CreateCommand<User>>,
            CreateUserBusinessRulesValidator>();
        services.AddTransient<IBusinessRulesValidator<UpdateCommand<User>>,
            UpdateUserBusinessRulesValidator>();
    }
}