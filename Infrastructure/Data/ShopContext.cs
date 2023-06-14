using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ShopContext : DbContext
{
    public ShopContext(DbContextOptions<ShopContext> options)
        : base(options)
    {
    }

    public DbSet<Brand> Brands { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<CustomerInfo> CustomersInfos { get; set; }
    public DbSet<ImageInfo> ImagesInfos { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderStatus> OrderStatuses { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductColor> ProductsColors { get; set; }
    public DbSet<ProductOption> ProductOptions { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Section> Sections { get; set; }
    public DbSet<Subcategory> Subcategories { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserType> UserTypes { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShopContext).Assembly);
        modelBuilder.UseIdentityAlwaysColumns();
    }
}