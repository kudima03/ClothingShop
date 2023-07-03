using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityConfiguration;

internal class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("orders");
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

        builder.Property(x => x.OrderStatusId)
               .HasColumnName("order_status_id")
               .IsRequired();

        builder.Property(x => x.DateTime)
               .HasColumnName("date_time")
               .IsRequired();

        builder.HasMany(x => x.OrderedProductsOptionsInfo)
               .WithOne(x => x.Order)
               .HasForeignKey(x => x.OrderId)
               .IsRequired();
    }
}