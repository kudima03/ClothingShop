using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityConfiguration;

internal class ImageInfoEntityTypeConfiguration : IEntityTypeConfiguration<ImageInfo>
{
    public void Configure(EntityTypeBuilder<ImageInfo> builder)
    {
        builder.ToTable("images_urls");
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

        builder.Property(x => x.Url)
               .HasColumnName("url")
               .IsRequired();

        builder.HasOne(x => x.ProductColor)
               .WithMany(x => x.ImagesInfos)
               .HasForeignKey(x => x.ProductColorId)
               .IsRequired();
    }
}