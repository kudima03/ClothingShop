using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityConfiguration;

internal class UserTypeEntityTypeConfiguration : IEntityTypeConfiguration<UserType>
{
    public void Configure(EntityTypeBuilder<UserType> builder)
    {
        builder.ToTable("user_types");
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
        builder.HasMany(x => x.Users)
            .WithOne(x => x.UserType)
            .HasForeignKey(x => x.UserTypeId)
            .IsRequired();
    }
}