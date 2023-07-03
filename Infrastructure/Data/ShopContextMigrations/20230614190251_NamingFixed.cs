#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.ShopContextMigrations;

/// <inheritdoc />
public partial class NamingFixed : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey
            ("FK_order_order_statuses_order_status_id",
             "order");

        migrationBuilder.DropForeignKey
            ("FK_order_users_user_id",
             "order");

        migrationBuilder.DropForeignKey
            ("FK_order_product_options_order_order_id",
             "order_product_options");

        migrationBuilder.DropForeignKey
            ("FK_order_product_options_product_option_product_option_id",
             "order_product_options");

        migrationBuilder.DropForeignKey
            ("FK_product_option_product_colors_product_color_id",
             "product_option");

        migrationBuilder.DropForeignKey
            ("FK_product_option_products_product_id",
             "product_option");

        migrationBuilder.DropTable("category_subcategories");

        migrationBuilder.DropPrimaryKey
            ("PK_product_option",
             "product_option");

        migrationBuilder.DropPrimaryKey
            ("PK_order_product_options",
             "order_product_options");

        migrationBuilder.DropPrimaryKey
            ("PK_order",
             "order");

        migrationBuilder.RenameTable
            ("product_option",
             newName: "product_options");

        migrationBuilder.RenameTable
            ("order_product_options",
             newName: "orders_product_options");

        migrationBuilder.RenameTable
            ("order",
             newName: "orders");

        migrationBuilder.RenameColumn
            ("Id",
             "customers_info",
             "id");

        migrationBuilder.RenameIndex
            ("IX_product_option_product_id",
             table: "product_options",
             newName: "IX_product_options_product_id");

        migrationBuilder.RenameIndex
            ("IX_product_option_product_color_id",
             table: "product_options",
             newName: "IX_product_options_product_color_id");

        migrationBuilder.RenameColumn
            ("Id",
             "orders_product_options",
             "id");

        migrationBuilder.RenameIndex
            ("IX_order_product_options_product_option_id",
             table: "orders_product_options",
             newName: "IX_orders_product_options_product_option_id");

        migrationBuilder.RenameIndex
            ("IX_order_product_options_order_id",
             table: "orders_product_options",
             newName: "IX_orders_product_options_order_id");

        migrationBuilder.RenameIndex
            ("IX_order_user_id",
             table: "orders",
             newName: "IX_orders_user_id");

        migrationBuilder.RenameIndex
            ("IX_order_order_status_id",
             table: "orders",
             newName: "IX_orders_order_status_id");

        migrationBuilder.AddPrimaryKey
            ("PK_product_options",
             "product_options",
             "id");

        migrationBuilder.AddPrimaryKey
            ("PK_orders_product_options",
             "orders_product_options",
             "id");

        migrationBuilder.AddPrimaryKey
            ("PK_orders",
             "orders",
             "id");

        migrationBuilder.CreateTable
            ("categories_subcategories",
             table => new
             {
                 category_id = table.Column<long>("bigint", nullable: false),
                 subcategory_id = table.Column<long>("bigint", nullable: false)
             },
             constraints: table =>
             {
                 table.PrimaryKey
                     ("PK_categories_subcategories",
                      x => new
                      {
                          x.category_id,
                          x.subcategory_id
                      });

                 table.ForeignKey
                     ("FK_categories_subcategories_categories_category_id",
                      x => x.category_id,
                      "categories",
                      "id",
                      onDelete: ReferentialAction.Cascade);

                 table.ForeignKey
                     ("FK_categories_subcategories_subcategories_subcategory_id",
                      x => x.subcategory_id,
                      "subcategories",
                      "id",
                      onDelete: ReferentialAction.Cascade);
             });

        migrationBuilder.CreateIndex
            ("IX_categories_subcategories_subcategory_id",
             "categories_subcategories",
             "subcategory_id");

        migrationBuilder.AddForeignKey
            ("FK_orders_order_statuses_order_status_id",
             "orders",
             "order_status_id",
             "order_statuses",
             principalColumn: "id",
             onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey
            ("FK_orders_users_user_id",
             "orders",
             "user_id",
             "users",
             principalColumn: "id",
             onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey
            ("FK_orders_product_options_orders_order_id",
             "orders_product_options",
             "order_id",
             "orders",
             principalColumn: "id",
             onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey
            ("FK_orders_product_options_product_options_product_option_id",
             "orders_product_options",
             "product_option_id",
             "product_options",
             principalColumn: "id",
             onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey
            ("FK_product_options_product_colors_product_color_id",
             "product_options",
             "product_color_id",
             "product_colors",
             principalColumn: "id",
             onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey
            ("FK_product_options_products_product_id",
             "product_options",
             "product_id",
             "products",
             principalColumn: "id",
             onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey
            ("FK_orders_order_statuses_order_status_id",
             "orders");

        migrationBuilder.DropForeignKey
            ("FK_orders_users_user_id",
             "orders");

        migrationBuilder.DropForeignKey
            ("FK_orders_product_options_orders_order_id",
             "orders_product_options");

        migrationBuilder.DropForeignKey
            ("FK_orders_product_options_product_options_product_option_id",
             "orders_product_options");

        migrationBuilder.DropForeignKey
            ("FK_product_options_product_colors_product_color_id",
             "product_options");

        migrationBuilder.DropForeignKey
            ("FK_product_options_products_product_id",
             "product_options");

        migrationBuilder.DropTable("categories_subcategories");

        migrationBuilder.DropPrimaryKey
            ("PK_product_options",
             "product_options");

        migrationBuilder.DropPrimaryKey
            ("PK_orders_product_options",
             "orders_product_options");

        migrationBuilder.DropPrimaryKey
            ("PK_orders",
             "orders");

        migrationBuilder.RenameTable
            ("product_options",
             newName: "product_option");

        migrationBuilder.RenameTable
            ("orders_product_options",
             newName: "order_product_options");

        migrationBuilder.RenameTable
            ("orders",
             newName: "order");

        migrationBuilder.RenameColumn
            ("id",
             "customers_info",
             "Id");

        migrationBuilder.RenameIndex
            ("IX_product_options_product_id",
             table: "product_option",
             newName: "IX_product_option_product_id");

        migrationBuilder.RenameIndex
            ("IX_product_options_product_color_id",
             table: "product_option",
             newName: "IX_product_option_product_color_id");

        migrationBuilder.RenameColumn
            ("id",
             "order_product_options",
             "Id");

        migrationBuilder.RenameIndex
            ("IX_orders_product_options_product_option_id",
             table: "order_product_options",
             newName: "IX_order_product_options_product_option_id");

        migrationBuilder.RenameIndex
            ("IX_orders_product_options_order_id",
             table: "order_product_options",
             newName: "IX_order_product_options_order_id");

        migrationBuilder.RenameIndex
            ("IX_orders_user_id",
             table: "order",
             newName: "IX_order_user_id");

        migrationBuilder.RenameIndex
            ("IX_orders_order_status_id",
             table: "order",
             newName: "IX_order_order_status_id");

        migrationBuilder.AddPrimaryKey
            ("PK_product_option",
             "product_option",
             "id");

        migrationBuilder.AddPrimaryKey
            ("PK_order_product_options",
             "order_product_options",
             "Id");

        migrationBuilder.AddPrimaryKey
            ("PK_order",
             "order",
             "id");

        migrationBuilder.CreateTable
            ("category_subcategories",
             table => new
             {
                 category_id = table.Column<long>("bigint", nullable: false),
                 subcategory_id = table.Column<long>("bigint", nullable: false)
             },
             constraints: table =>
             {
                 table.PrimaryKey
                     ("PK_category_subcategories",
                      x => new
                      {
                          x.category_id,
                          x.subcategory_id
                      });

                 table.ForeignKey
                     ("FK_category_subcategories_categories_category_id",
                      x => x.category_id,
                      "categories",
                      "id",
                      onDelete: ReferentialAction.Cascade);

                 table.ForeignKey
                     ("FK_category_subcategories_subcategories_subcategory_id",
                      x => x.subcategory_id,
                      "subcategories",
                      "id",
                      onDelete: ReferentialAction.Cascade);
             });

        migrationBuilder.CreateIndex
            ("IX_category_subcategories_subcategory_id",
             "category_subcategories",
             "subcategory_id");

        migrationBuilder.AddForeignKey
            ("FK_order_order_statuses_order_status_id",
             "order",
             "order_status_id",
             "order_statuses",
             principalColumn: "id",
             onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey
            ("FK_order_users_user_id",
             "order",
             "user_id",
             "users",
             principalColumn: "id",
             onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey
            ("FK_order_product_options_order_order_id",
             "order_product_options",
             "order_id",
             "order",
             principalColumn: "id",
             onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey
            ("FK_order_product_options_product_option_product_option_id",
             "order_product_options",
             "product_option_id",
             "product_option",
             principalColumn: "id",
             onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey
            ("FK_product_option_product_colors_product_color_id",
             "product_option",
             "product_color_id",
             "product_colors",
             principalColumn: "id",
             onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey
            ("FK_product_option_products_product_id",
             "product_option",
             "product_id",
             "products",
             principalColumn: "id",
             onDelete: ReferentialAction.Cascade);
    }
}