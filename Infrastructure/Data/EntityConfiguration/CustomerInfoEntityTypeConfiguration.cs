using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityConfiguration;

internal class CustomerInfoEntityTypeConfiguration : IEntityTypeConfiguration<CustomerInfo>
{
    public void Configure(EntityTypeBuilder<CustomerInfo> builder)
    {
        builder.ToTable("customers_info");
        builder.HasOne(x => x.User)
            .WithOne()
            .IsRequired();
        builder.HasKey(x => x.Id);
        builder.Property(x => x.UserId)
            .HasColumnName("user_id")
            .IsRequired();
        builder.Property(x => x.Name)
            .HasColumnName("name")
            .IsRequired(false);
        builder.Property(x => x.Surname)
            .HasColumnName("surname")
            .IsRequired(false);
        builder.Property(x => x.Patronymic)
            .HasColumnName("patronymic")
            .IsRequired(false);
        builder.Property(x => x.Address)
            .HasColumnName("address")
            .IsRequired(false);
        builder.Property(x => x.Phone)
            .HasColumnName("phone")
            .IsRequired(false);
    }
}