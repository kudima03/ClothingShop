using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityConfiguration;

internal class ProductOptionEntityTypeConfiguration : IEntityTypeConfiguration<ProductOption>
{
    public void Configure(EntityTypeBuilder<ProductOption> builder)
    {
        builder.ToTable("product_option");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .HasColumnName("id")
               .IsRequired()
               .ValueGeneratedOnAdd()
               .Metadata
               .SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);

        builder.Property(x => x.ProductColorId)
               .HasColumnName("product_color_id")
               .IsRequired();

        builder.Property(x => x.ProductId)
               .HasColumnName("product_id")
               .IsRequired();

        builder.Property(x => x.Price)
               .HasColumnName("price")
               .IsRequired();

        builder.Property(x => x.Quantity)
               .HasColumnName("quantity")
               .IsRequired();

        builder.Property(x => x.Size)
               .HasColumnName("size")
               .IsRequired();

        builder.HasOne(x => x.Product)
               .WithMany(x => x.ProductOptions)
               .HasForeignKey(x => x.ProductId)
               .IsRequired();

        builder.HasMany(x => x.OrderedProductOptions)
               .WithOne(x => x.ProductOption)
               .HasForeignKey(x => x.ProductOptionId)
               .IsRequired();
    }
}