using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityConfiguration;

internal class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("order");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("id")
            .IsRequired()
            .ValueGeneratedOnAdd()
            .Metadata
            .SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);
        builder.Property(x => x.UserId)
            .HasColumnName("user_id")
            .IsRequired();
        builder.Property(x => x.OrderStatusId)
            .HasColumnName("order_status_id")
            .IsRequired();
        builder.Property(x => x.DateTime)
            .HasColumnName("date_time")
            .IsRequired();
        builder.HasOne(x => x.User)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.UserId)
            .IsRequired();
        builder.HasMany(x => x.ProductsOptions).WithMany(x => x.Orders)
            .UsingEntity("orders_product_options",
                l => l.HasOne(typeof(Order)).WithMany().HasForeignKey("order_id"),
                r => r.HasOne(typeof(ProductOption)).WithMany().HasForeignKey("product_option_id"),
                amount => amount.Property(typeof(int), "amount"));
    }
}