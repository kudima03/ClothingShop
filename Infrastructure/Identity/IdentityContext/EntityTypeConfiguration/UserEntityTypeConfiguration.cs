using Infrastructure.Identity.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Identity.IdentityContext.EntityTypeConfiguration;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
               .IsRequired(false);

        builder.Property(x => x.Surname)
               .IsRequired(false);

        builder.Property(x => x.Patronymic)
               .IsRequired(false);

        builder.Property(x => x.Address)
               .IsRequired(false);

        builder.Property(x => x.Email)
               .IsRequired();

        builder.Property(x => x.DeletionDateTime)
               .IsRequired(false);
    }
}