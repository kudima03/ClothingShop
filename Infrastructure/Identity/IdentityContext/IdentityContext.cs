using Infrastructure.Identity.Entity;
using Infrastructure.Identity.IdentityContext.EntityTypeConfiguration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity.IdentityContext;

public class IdentityContext : IdentityDbContext<User, IdentityRole<long>, long>
{
    public IdentityContext(DbContextOptions<IdentityContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new UserEntityTypeConfiguration());
        base.OnModelCreating(builder);
    }
}