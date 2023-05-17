using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityConfiguration;

internal class ColorEntityTypeConfiguration : IEntityTypeConfiguration<Color>
{
    public void Configure(EntityTypeBuilder<Color> builder)
    {
        builder.ToTable("colors");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("id")
            .IsRequired()
            .ValueGeneratedOnAdd()
            .Metadata
            .SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);
        builder.Property(x => x.Name)
            .HasColumnName("name")
            .IsRequired();
        builder.Property(x => x.Hex)
            .HasColumnName("hex")
            .IsRequired();
        builder.HasMany(x => x.ProductColors)
            .WithOne(x => x.Color)
            .HasForeignKey(x => x.ColorId)
            .IsRequired();
    }
}