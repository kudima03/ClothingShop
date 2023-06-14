using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityConfiguration;

internal class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("id")
            .IsRequired()
            .ValueGeneratedOnAdd()
            .Metadata
            .SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);
        builder.HasIndex(x => x.Name)
            .IsUnique();
        builder.Property(x => x.Name)
            .HasColumnName("name")
            .IsRequired();
        builder.Property(x => x.BrandId)
            .HasColumnName("brand_id")
            .IsRequired();
        builder.Property(x => x.SubcategoryId)
            .HasColumnName("subcategory_id")
            .IsRequired();
        builder.HasOne(x => x.Brand)
            .WithMany(x => x.Products)
            .HasForeignKey(x => x.BrandId)
            .IsRequired();
        builder.HasOne(x => x.Subcategory)
            .WithMany(x => x.Products)
            .HasForeignKey(x => x.SubcategoryId)
            .IsRequired();
        builder.HasMany(x => x.ProductOptions)
            .WithOne(x => x.Product)
            .HasForeignKey(x => x.ProductId)
            .IsRequired();
    }
}