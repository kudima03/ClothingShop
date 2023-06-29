using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityConfiguration;
public class ShoppingCartItemEntityTypeConfiguration : IEntityTypeConfiguration<ShoppingCartItem>
{
    public void Configure(EntityTypeBuilder<ShoppingCartItem> builder)
    {
        builder.ToTable("shopping_carts_product_options");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("id")
            .IsRequired();
        builder.Property(x => x.ShoppingCartId)
            .HasColumnName("shopping_cart_id")
            .IsRequired();
        builder.Property(x => x.ShoppingCartId)
            .HasColumnName("product_option_id")
            .IsRequired();
        builder.Property(x => x.Amount)
            .HasColumnName("amount")
            .IsRequired();
        builder.Property(x => x.CreationDateTime)
            .HasColumnName("creation_date_time")
            .IsRequired();
        builder.HasOne(x => x.ShoppingCart)
            .WithMany(x => x.Items)
            .HasForeignKey(x => x.ShoppingCartId);
        builder.HasOne(x => x.ProductOption)
            .WithMany(x => x.ReservedProductOptions)
            .HasForeignKey(x => x.ProductOptionId);
    }
}