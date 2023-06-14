using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityConfiguration;

public class OrderedProductOptionInfoEntityTypeConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("order_product_options");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ProductOptionId).HasColumnName("product_option_id").IsRequired();

        builder.Property(x => x.OrderId).HasColumnName("order_id").IsRequired();

        builder.Property(x => x.Amount).HasColumnName("amount").IsRequired();

        builder.HasOne(x => x.ProductOption)
               .WithMany(x => x.OrderedProductOptions)
               .HasForeignKey(x => x.ProductOptionId);

        builder.HasOne(x => x.Order)
               .WithMany(x => x.OrderedProductsOptionsInfo)
               .HasForeignKey(x => x.OrderId);
    }
}