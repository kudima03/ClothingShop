using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityConfiguration;

internal class ReviewEntityTypeConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("reviews");
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

        builder.Property(x => x.ProductId)
               .HasColumnName("product_id")
               .IsRequired();

        builder.Property(x => x.Comment)
               .HasColumnName("comment")
               .IsRequired(false);

        builder.Property(x => x.DateTime)
               .HasColumnName("date_time")
               .IsRequired();

        builder.Property(x => x.Rate)
               .HasColumnName("rate")
               .IsRequired();

        builder.HasOne(x => x.User)
               .WithMany(x => x.Reviews)
               .HasForeignKey(x => x.UserId)
               .IsRequired();

        builder.HasOne(x => x.Product)
               .WithMany(x => x.Reviews)
               .HasForeignKey(x => x.ProductId)
               .IsRequired();
    }
}