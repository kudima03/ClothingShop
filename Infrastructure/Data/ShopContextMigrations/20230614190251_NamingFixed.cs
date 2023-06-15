using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.ShopContextMigrations
{
    /// <inheritdoc />
    public partial class NamingFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_order_statuses_order_status_id",
                table: "order");

            migrationBuilder.DropForeignKey(
                name: "FK_order_users_user_id",
                table: "order");

            migrationBuilder.DropForeignKey(
                name: "FK_order_product_options_order_order_id",
                table: "order_product_options");

            migrationBuilder.DropForeignKey(
                name: "FK_order_product_options_product_option_product_option_id",
                table: "order_product_options");

            migrationBuilder.DropForeignKey(
                name: "FK_product_option_product_colors_product_color_id",
                table: "product_option");

            migrationBuilder.DropForeignKey(
                name: "FK_product_option_products_product_id",
                table: "product_option");

            migrationBuilder.DropTable(
                name: "category_subcategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_product_option",
                table: "product_option");

            migrationBuilder.DropPrimaryKey(
                name: "PK_order_product_options",
                table: "order_product_options");

            migrationBuilder.DropPrimaryKey(
                name: "PK_order",
                table: "order");

            migrationBuilder.RenameTable(
                name: "product_option",
                newName: "product_options");

            migrationBuilder.RenameTable(
                name: "order_product_options",
                newName: "orders_product_options");

            migrationBuilder.RenameTable(
                name: "order",
                newName: "orders");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "customers_info",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_product_option_product_id",
                table: "product_options",
                newName: "IX_product_options_product_id");

            migrationBuilder.RenameIndex(
                name: "IX_product_option_product_color_id",
                table: "product_options",
                newName: "IX_product_options_product_color_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "orders_product_options",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_order_product_options_product_option_id",
                table: "orders_product_options",
                newName: "IX_orders_product_options_product_option_id");

            migrationBuilder.RenameIndex(
                name: "IX_order_product_options_order_id",
                table: "orders_product_options",
                newName: "IX_orders_product_options_order_id");

            migrationBuilder.RenameIndex(
                name: "IX_order_user_id",
                table: "orders",
                newName: "IX_orders_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_order_order_status_id",
                table: "orders",
                newName: "IX_orders_order_status_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_product_options",
                table: "product_options",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_orders_product_options",
                table: "orders_product_options",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_orders",
                table: "orders",
                column: "id");

            migrationBuilder.CreateTable(
                name: "categories_subcategories",
                columns: table => new
                {
                    category_id = table.Column<long>(type: "bigint", nullable: false),
                    subcategory_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories_subcategories", x => new { x.category_id, x.subcategory_id });
                    table.ForeignKey(
                        name: "FK_categories_subcategories_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_categories_subcategories_subcategories_subcategory_id",
                        column: x => x.subcategory_id,
                        principalTable: "subcategories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_categories_subcategories_subcategory_id",
                table: "categories_subcategories",
                column: "subcategory_id");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_order_statuses_order_status_id",
                table: "orders",
                column: "order_status_id",
                principalTable: "order_statuses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_users_user_id",
                table: "orders",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_product_options_orders_order_id",
                table: "orders_product_options",
                column: "order_id",
                principalTable: "orders",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_product_options_product_options_product_option_id",
                table: "orders_product_options",
                column: "product_option_id",
                principalTable: "product_options",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_product_options_product_colors_product_color_id",
                table: "product_options",
                column: "product_color_id",
                principalTable: "product_colors",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_product_options_products_product_id",
                table: "product_options",
                column: "product_id",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_order_statuses_order_status_id",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_users_user_id",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_product_options_orders_order_id",
                table: "orders_product_options");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_product_options_product_options_product_option_id",
                table: "orders_product_options");

            migrationBuilder.DropForeignKey(
                name: "FK_product_options_product_colors_product_color_id",
                table: "product_options");

            migrationBuilder.DropForeignKey(
                name: "FK_product_options_products_product_id",
                table: "product_options");

            migrationBuilder.DropTable(
                name: "categories_subcategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_product_options",
                table: "product_options");

            migrationBuilder.DropPrimaryKey(
                name: "PK_orders_product_options",
                table: "orders_product_options");

            migrationBuilder.DropPrimaryKey(
                name: "PK_orders",
                table: "orders");

            migrationBuilder.RenameTable(
                name: "product_options",
                newName: "product_option");

            migrationBuilder.RenameTable(
                name: "orders_product_options",
                newName: "order_product_options");

            migrationBuilder.RenameTable(
                name: "orders",
                newName: "order");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "customers_info",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_product_options_product_id",
                table: "product_option",
                newName: "IX_product_option_product_id");

            migrationBuilder.RenameIndex(
                name: "IX_product_options_product_color_id",
                table: "product_option",
                newName: "IX_product_option_product_color_id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "order_product_options",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_orders_product_options_product_option_id",
                table: "order_product_options",
                newName: "IX_order_product_options_product_option_id");

            migrationBuilder.RenameIndex(
                name: "IX_orders_product_options_order_id",
                table: "order_product_options",
                newName: "IX_order_product_options_order_id");

            migrationBuilder.RenameIndex(
                name: "IX_orders_user_id",
                table: "order",
                newName: "IX_order_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_orders_order_status_id",
                table: "order",
                newName: "IX_order_order_status_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_product_option",
                table: "product_option",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_order_product_options",
                table: "order_product_options",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_order",
                table: "order",
                column: "id");

            migrationBuilder.CreateTable(
                name: "category_subcategories",
                columns: table => new
                {
                    category_id = table.Column<long>(type: "bigint", nullable: false),
                    subcategory_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category_subcategories", x => new { x.category_id, x.subcategory_id });
                    table.ForeignKey(
                        name: "FK_category_subcategories_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_category_subcategories_subcategories_subcategory_id",
                        column: x => x.subcategory_id,
                        principalTable: "subcategories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_category_subcategories_subcategory_id",
                table: "category_subcategories",
                column: "subcategory_id");

            migrationBuilder.AddForeignKey(
                name: "FK_order_order_statuses_order_status_id",
                table: "order",
                column: "order_status_id",
                principalTable: "order_statuses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_order_users_user_id",
                table: "order",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_order_product_options_order_order_id",
                table: "order_product_options",
                column: "order_id",
                principalTable: "order",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_order_product_options_product_option_product_option_id",
                table: "order_product_options",
                column: "product_option_id",
                principalTable: "product_option",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_product_option_product_colors_product_color_id",
                table: "product_option",
                column: "product_color_id",
                principalTable: "product_colors",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_product_option_products_product_id",
                table: "product_option",
                column: "product_id",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
