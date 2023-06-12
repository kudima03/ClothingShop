using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityConfiguration;

internal class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .Metadata
            .SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);
        builder.Property(x => x.UserTypeId)
            .HasColumnName("user_type_id")
            .IsRequired();
        builder.HasIndex(x => x.Email)
            .IsUnique();
        builder.Property(x => x.Email)
            .HasColumnName("email")
            .IsRequired();
        builder.Property(x => x.Password)
            .HasColumnName("password")
            .IsRequired();
        builder.Property(x => x.DeletionDateTime)
            .HasColumnName("deletion_date_time")
            .IsRequired(false);
        builder.HasOne(x => x.UserType)
            .WithMany(x => x.Users)
            .HasForeignKey(x => x.UserTypeId)
            .IsRequired();
    }
}