#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infrastructure.Data.ShopContextMigrations;

/// <inheritdoc />
public partial class OrderedProductOptionInfoEntityAdded : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "orders_product_options");

        migrationBuilder.CreateTable(
            "order_product_options",
            table => new
            {
                Id = table.Column<long>("bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                order_id = table.Column<long>("bigint", nullable: false),
                product_option_id = table.Column<long>("bigint", nullable: false),
                amount = table.Column<int>("integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_order_product_options", x => x.Id);
                table.ForeignKey(
                    "FK_order_product_options_order_order_id",
                    x => x.order_id,
                    "order",
                    "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    "FK_order_product_options_product_option_product_option_id",
                    x => x.product_option_id,
                    "product_option",
                    "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            "IX_order_product_options_order_id",
            "order_product_options",
            "order_id");

        migrationBuilder.CreateIndex(
            "IX_order_product_options_product_option_id",
            "order_product_options",
            "product_option_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "order_product_options");

        migrationBuilder.CreateTable(
            "orders_product_options",
            table => new
            {
                order_id = table.Column<long>("bigint", nullable: false),
                product_option_id = table.Column<long>("bigint", nullable: false),
                amount = table.Column<int>("integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_orders_product_options", x => new { x.order_id, x.product_option_id });
                table.ForeignKey(
                    "FK_orders_product_options_order_order_id",
                    x => x.order_id,
                    "order",
                    "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    "FK_orders_product_options_product_option_product_option_id",
                    x => x.product_option_id,
                    "product_option",
                    "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            "IX_orders_product_options_product_option_id",
            "orders_product_options",
            "product_option_id");
    }
}