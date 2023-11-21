using Infrastructure.Identity.Entity;
using Infrastructure.Identity.IdentityContext.EntityTypeConfiguration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity.IdentityContext;

public class IdentityContext(DbContextOptions<IdentityContext> options) : IdentityDbContext<User, IdentityRole<long>, long>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new UserEntityTypeConfiguration());
        base.OnModelCreating(builder);
    }
}