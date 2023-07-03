#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infrastructure.Data.ShopContextMigrations;

/// <inheritdoc />
public partial class ShoppingCartIntegrated : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey
            ("FK_orders_users_user_id",
             "orders");

        migrationBuilder.DropForeignKey
            ("FK_reviews_users_user_id",
             "reviews");

        migrationBuilder.DropTable("customers_info");

        migrationBuilder.DropTable("users");

        migrationBuilder.DropTable("user_types");

        migrationBuilder.DropIndex
            ("IX_reviews_user_id",
             "reviews");

        migrationBuilder.DropIndex
            ("IX_orders_user_id",
             "orders");

        migrationBuilder.CreateTable
            ("shopping_carts",
             table => new
             {
                 id = table.Column<long>("bigint", nullable: false)
                           .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                 user_id = table.Column<long>("bigint", nullable: false)
             },
             constraints: table =>
             {
                 table.PrimaryKey("PK_shopping_carts", x => x.id);
             });

        migrationBuilder.CreateTable
            ("shopping_carts_product_options",
             table => new
             {
                 id = table.Column<long>("bigint", nullable: false)
                           .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                 product_option_id = table.Column<long>("bigint", nullable: false),
                 ProductOptionId = table.Column<long>("bigint", nullable: false),
                 amount = table.Column<int>("integer", nullable: false)
             },
             constraints: table =>
             {
                 table.PrimaryKey("PK_shopping_carts_product_options", x => x.id);

                 table.ForeignKey
                     ("FK_shopping_carts_product_options_product_options_ProductOptio~",
                      x => x.ProductOptionId,
                      "product_options",
                      "id",
                      onDelete: ReferentialAction.Cascade);

                 table.ForeignKey
                     ("FK_shopping_carts_product_options_shopping_carts_product_optio~",
                      x => x.product_option_id,
                      "shopping_carts",
                      "id",
                      onDelete: ReferentialAction.Cascade);
             });

        migrationBuilder.CreateIndex
            ("IX_shopping_carts_product_options_product_option_id",
             "shopping_carts_product_options",
             "product_option_id");

        migrationBuilder.CreateIndex
            ("IX_shopping_carts_product_options_ProductOptionId",
             "shopping_carts_product_options",
             "ProductOptionId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable("shopping_carts_product_options");

        migrationBuilder.DropTable("shopping_carts");

        migrationBuilder.CreateTable
            ("user_types",
             table => new
             {
                 id = table.Column<long>("bigint", nullable: false)
                           .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                 name = table.Column<string>("text", nullable: false)
             },
             constraints: table =>
             {
                 table.PrimaryKey("PK_user_types", x => x.id);
             });

        migrationBuilder.CreateTable
            ("users",
             table => new
             {
                 id = table.Column<long>("bigint", nullable: false)
                           .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                 user_type_id = table.Column<long>("bigint", nullable: false),
                 deletion_date_time = table.Column<DateTime>("timestamp with time zone", nullable: true),
                 email = table.Column<string>("text", nullable: false),
                 password = table.Column<string>("text", nullable: false)
             },
             constraints: table =>
             {
                 table.PrimaryKey("PK_users", x => x.id);

                 table.ForeignKey
                     ("FK_users_user_types_user_type_id",
                      x => x.user_type_id,
                      "user_types",
                      "id",
                      onDelete: ReferentialAction.Cascade);
             });

        migrationBuilder.CreateTable
            ("customers_info",
             table => new
             {
                 id = table.Column<long>("bigint", nullable: false)
                           .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                 user_id = table.Column<long>("bigint", nullable: false),
                 address = table.Column<string>("text", nullable: true),
                 name = table.Column<string>("text", nullable: true),
                 patronymic = table.Column<string>("text", nullable: true),
                 phone = table.Column<string>("text", nullable: true),
                 surname = table.Column<string>("text", nullable: true)
             },
             constraints: table =>
             {
                 table.PrimaryKey("PK_customers_info", x => x.id);

                 table.ForeignKey
                     ("FK_customers_info_users_user_id",
                      x => x.user_id,
                      "users",
                      "id",
                      onDelete: ReferentialAction.Cascade);
             });

        migrationBuilder.CreateIndex
            ("IX_reviews_user_id",
             "reviews",
             "user_id");

        migrationBuilder.CreateIndex
            ("IX_orders_user_id",
             "orders",
             "user_id");

        migrationBuilder.CreateIndex
            ("IX_customers_info_user_id",
             "customers_info",
             "user_id",
             unique: true);

        migrationBuilder.CreateIndex
            ("IX_users_email",
             "users",
             "email",
             unique: true);

        migrationBuilder.CreateIndex
            ("IX_users_user_type_id",
             "users",
             "user_type_id");

        migrationBuilder.AddForeignKey
            ("FK_orders_users_user_id",
             "orders",
             "user_id",
             "users",
             principalColumn: "id",
             onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey
            ("FK_reviews_users_user_id",
             "reviews",
             "user_id",
             "users",
             principalColumn: "id",
             onDelete: ReferentialAction.Cascade);
    }
}