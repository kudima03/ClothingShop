#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.ShopContextMigrations;

/// <inheritdoc />
public partial class UniqueConstraintsAdded : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateIndex(
            "IX_users_email",
            "users",
            "email",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_subcategories_name",
            "subcategories",
            "name",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_sections_name",
            "sections",
            "name",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_products_name",
            "products",
            "name",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_categories_name",
            "categories",
            "name",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_brands_name",
            "brands",
            "name",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            "IX_users_email",
            "users");

        migrationBuilder.DropIndex(
            "IX_subcategories_name",
            "subcategories");

        migrationBuilder.DropIndex(
            "IX_sections_name",
            "sections");

        migrationBuilder.DropIndex(
            "IX_products_name",
            "products");

        migrationBuilder.DropIndex(
            "IX_categories_name",
            "categories");

        migrationBuilder.DropIndex(
            "IX_brands_name",
            "brands");
    }
}