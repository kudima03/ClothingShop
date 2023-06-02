using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityConfiguration;

internal class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("categories");
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
        builder
            .HasMany(x => x.Subcategories)
            .WithMany(x => x.Categories)
            .UsingEntity("category_subcategories",
                l => l.HasOne(typeof(Category)).WithMany().HasForeignKey("category_id"),
                r => r.HasOne(typeof(Subcategory)).WithMany().HasForeignKey("subcategory_id"));

        builder.HasMany(x => x.Sections)
            .WithMany(x => x.Categories)
            .UsingEntity("sections_categories",
                l => l.HasOne(typeof(Section)).WithMany().HasForeignKey("section_id"),
                r => r.HasOne(typeof(Category)).WithMany().HasForeignKey("category_id"));
    }
}