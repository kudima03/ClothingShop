using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityConfiguration;

internal class SubcategoryEntityTypeConfiguration
    : IEntityTypeConfiguration<Subcategory>
{
    public void Configure(EntityTypeBuilder<Subcategory> builder)
    {
        builder.ToTable("subcategories");
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
        builder.HasMany(x => x.Categories)
            .WithMany(x => x.Subcategories)
            .UsingEntity("category_subcategories",
                l => l.HasOne(typeof(Category)).WithMany().HasForeignKey("category_id"),
                r => r.HasOne(typeof(Subcategory)).WithMany().HasForeignKey("subcategory_id"));
        builder.HasMany(x => x.Products)
            .WithOne(x => x.Subcategory)
            .HasForeignKey(x => x.SubcategoryId)
            .IsRequired();
    }
}