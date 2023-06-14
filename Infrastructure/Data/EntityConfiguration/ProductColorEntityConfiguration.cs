using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityConfiguration;

internal class ProductColorEntityConfiguration : IEntityTypeConfiguration<ProductColor>
{
    public void Configure(EntityTypeBuilder<ProductColor> builder)
    {
        builder.ToTable("product_colors");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .HasColumnName("id")
               .IsRequired()
               .ValueGeneratedOnAdd()
               .Metadata
               .SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);

        builder.Property(x => x.ColorHex)
               .HasColumnName("color_hex")
               .IsRequired();

        builder.HasMany(x => x.ImagesInfos)
               .WithOne(x => x.ProductColor)
               .HasForeignKey(x => x.ProductColorId)
               .IsRequired();

        builder.HasMany(x => x.ProductOptions)
               .WithOne(x => x.ProductColor)
               .HasForeignKey(x => x.ProductColorId)
               .IsRequired();
    }
}