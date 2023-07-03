using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityConfiguration;

internal class OrderStatusEntityTypeConfiguration : IEntityTypeConfiguration<OrderStatus>
{
    public void Configure(EntityTypeBuilder<OrderStatus> builder)
    {
        builder.ToTable("order_statuses");
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
    }
}