#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.ShopContextMigrations;

/// <inheritdoc />
public partial class ColumnNamingFixed : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey
            ("FK_shopping_carts_product_options_product_options_ProductOptio~",
             "shopping_carts_product_options");

        migrationBuilder.DropForeignKey
            ("FK_shopping_carts_product_options_shopping_carts_product_optio~",
             "shopping_carts_product_options");

        migrationBuilder.RenameColumn
            ("ProductOptionId",
             "shopping_carts_product_options",
             "shopping_cart_id");

        migrationBuilder.RenameIndex
            ("IX_shopping_carts_product_options_ProductOptionId",
             table: "shopping_carts_product_options",
             newName: "IX_shopping_carts_product_options_shopping_cart_id");

        migrationBuilder.AddForeignKey
            ("FK_shopping_carts_product_options_product_options_product_opti~",
             "shopping_carts_product_options",
             "product_option_id",
             "product_options",
             principalColumn: "id",
             onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey
            ("FK_shopping_carts_product_options_shopping_carts_shopping_cart~",
             "shopping_carts_product_options",
             "shopping_cart_id",
             "shopping_carts",
             principalColumn: "id",
             onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey
            ("FK_shopping_carts_product_options_product_options_product_opti~",
             "shopping_carts_product_options");

        migrationBuilder.DropForeignKey
            ("FK_shopping_carts_product_options_shopping_carts_shopping_cart~",
             "shopping_carts_product_options");

        migrationBuilder.RenameColumn
            ("shopping_cart_id",
             "shopping_carts_product_options",
             "ProductOptionId");

        migrationBuilder.RenameIndex
            ("IX_shopping_carts_product_options_shopping_cart_id",
             table: "shopping_carts_product_options",
             newName: "IX_shopping_carts_product_options_ProductOptionId");

        migrationBuilder.AddForeignKey
            ("FK_shopping_carts_product_options_product_options_ProductOptio~",
             "shopping_carts_product_options",
             "ProductOptionId",
             "product_options",
             principalColumn: "id",
             onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey
            ("FK_shopping_carts_product_options_shopping_carts_product_optio~",
             "shopping_carts_product_options",
             "product_option_id",
             "shopping_carts",
             principalColumn: "id",
             onDelete: ReferentialAction.Cascade);
    }
}