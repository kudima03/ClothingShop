using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.ShopContextMigrations
{
    /// <inheritdoc />
    public partial class UniqueConstraintsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_users_email",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_subcategories_name",
                table: "subcategories",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_sections_name",
                table: "sections",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_products_name",
                table: "products",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_categories_name",
                table: "categories",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_brands_name",
                table: "brands",
                column: "name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_users_email",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_subcategories_name",
                table: "subcategories");

            migrationBuilder.DropIndex(
                name: "IX_sections_name",
                table: "sections");

            migrationBuilder.DropIndex(
                name: "IX_products_name",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_categories_name",
                table: "categories");

            migrationBuilder.DropIndex(
                name: "IX_brands_name",
                table: "brands");
        }
    }
}
