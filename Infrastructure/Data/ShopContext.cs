using ApplicationCore.Entities;
using Infrastructure.Data.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ShopContext(DbContextOptions<ShopContext> options) : DbContext(options)
{
    public DbSet<Brand> Brands { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<ImageInfo> ImagesInfos { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderStatus> OrderStatuses { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<ProductColor> ProductsColors { get; set; }

    public DbSet<ProductOption> ProductOptions { get; set; }

    public DbSet<Review> Reviews { get; set; }

    public DbSet<Section> Sections { get; set; }

    public DbSet<ShoppingCart> ShoppingCarts { get; set; }

    public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

    public DbSet<Subcategory> Subcategories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BrandEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ImageInfoEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new OrderEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new OrderItemEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new OrderStatusEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ProductColorEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ProductEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ProductOptionEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ReviewEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new SectionEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ShoppingCartEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ShoppingCartItemEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new SubcategoryEntityTypeConfiguration());
        modelBuilder.UseIdentityAlwaysColumns();
        base.OnModelCreating(modelBuilder);
    }
}