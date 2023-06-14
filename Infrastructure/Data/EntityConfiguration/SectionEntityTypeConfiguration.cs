using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityConfiguration;

internal class SectionEntityTypeConfiguration
    : IEntityTypeConfiguration<Section>
{
    public void Configure(EntityTypeBuilder<Section> builder)
    {
        builder.ToTable("sections");
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

        builder.HasMany(x => x.Categories)
               .WithMany(x => x.Sections)
               .UsingEntity("sections_categories",
                            l => l.HasOne(typeof(Section)).WithMany().HasForeignKey("section_id"),
                            r => r.HasOne(typeof(Category)).WithMany().HasForeignKey("category_id"));
    }
}