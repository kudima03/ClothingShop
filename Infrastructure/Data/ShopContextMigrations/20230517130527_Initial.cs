#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infrastructure.Data.ShopContextMigrations;

/// <inheritdoc />
public partial class Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable("brands",
                                     table => new
                                     {
                                         id = table.Column<long>("bigint", nullable: false)
                                                   .Annotation("Npgsql:ValueGenerationStrategy",
                                                               NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                                         name = table.Column<string>("text", nullable: false)
                                     },
                                     constraints: table =>
                                     {
                                         table.PrimaryKey("PK_brands", x => x.id);
                                     });

        migrationBuilder.CreateTable("categories",
                                     table => new
                                     {
                                         id = table.Column<long>("bigint", nullable: false)
                                                   .Annotation("Npgsql:ValueGenerationStrategy",
                                                               NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                                         name = table.Column<string>("text", nullable: false)
                                     },
                                     constraints: table =>
                                     {
                                         table.PrimaryKey("PK_categories", x => x.id);
                                     });

        migrationBuilder.CreateTable("colors",
                                     table => new
                                     {
                                         id = table.Column<long>("bigint", nullable: false)
                                                   .Annotation("Npgsql:ValueGenerationStrategy",
                                                               NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                                         name = table.Column<string>("text", nullable: false),
                                         hex = table.Column<string>("text", nullable: false)
                                     },
                                     constraints: table =>
                                     {
                                         table.PrimaryKey("PK_colors", x => x.id);
                                     });

        migrationBuilder.CreateTable("order_statuses",
                                     table => new
                                     {
                                         id = table.Column<long>("bigint", nullable: false)
                                                   .Annotation("Npgsql:ValueGenerationStrategy",
                                                               NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                                         name = table.Column<string>("text", nullable: false)
                                     },
                                     constraints: table =>
                                     {
                                         table.PrimaryKey("PK_order_statuses", x => x.id);
                                     });

        migrationBuilder.CreateTable("sections",
                                     table => new
                                     {
                                         id = table.Column<long>("bigint", nullable: false)
                                                   .Annotation("Npgsql:ValueGenerationStrategy",
                                                               NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                                         name = table.Column<string>("text", nullable: false)
                                     },
                                     constraints: table =>
                                     {
                                         table.PrimaryKey("PK_sections", x => x.id);
                                     });

        migrationBuilder.CreateTable("subcategories",
                                     table => new
                                     {
                                         id = table.Column<long>("bigint", nullable: false)
                                                   .Annotation("Npgsql:ValueGenerationStrategy",
                                                               NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                                         name = table.Column<string>("text", nullable: false)
                                     },
                                     constraints: table =>
                                     {
                                         table.PrimaryKey("PK_subcategories", x => x.id);
                                     });

        migrationBuilder.CreateTable("user_types",
                                     table => new
                                     {
                                         id = table.Column<long>("bigint", nullable: false)
                                                   .Annotation("Npgsql:ValueGenerationStrategy",
                                                               NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                                         name = table.Column<string>("text", nullable: false)
                                     },
                                     constraints: table =>
                                     {
                                         table.PrimaryKey("PK_user_types", x => x.id);
                                     });

        migrationBuilder.CreateTable("product_colors",
                                     table => new
                                     {
                                         id = table.Column<long>("bigint", nullable: false),
                                         color_id = table.Column<long>("bigint", nullable: false)
                                     },
                                     constraints: table =>
                                     {
                                         table.PrimaryKey("PK_product_colors", x => x.id);

                                         table.ForeignKey("FK_product_colors_colors_id",
                                                          x => x.id,
                                                          "colors",
                                                          "id",
                                                          onDelete: ReferentialAction.Cascade);
                                     });

        migrationBuilder.CreateTable("sections_categories",
                                     table => new
                                     {
                                         category_id = table.Column<long>("bigint", nullable: false),
                                         section_id = table.Column<long>("bigint", nullable: false)
                                     },
                                     constraints: table =>
                                     {
                                         table.PrimaryKey("PK_sections_categories",
                                                          x => new
                                                          {
                                                              x.category_id,
                                                              x.section_id
                                                          });

                                         table.ForeignKey("FK_sections_categories_categories_category_id",
                                                          x => x.category_id,
                                                          "categories",
                                                          "id",
                                                          onDelete: ReferentialAction.Cascade);

                                         table.ForeignKey("FK_sections_categories_sections_section_id",
                                                          x => x.section_id,
                                                          "sections",
                                                          "id",
                                                          onDelete: ReferentialAction.Cascade);
                                     });

        migrationBuilder.CreateTable("category_subcategories",
                                     table => new
                                     {
                                         category_id = table.Column<long>("bigint", nullable: false),
                                         subcategory_id = table.Column<long>("bigint", nullable: false)
                                     },
                                     constraints: table =>
                                     {
                                         table.PrimaryKey("PK_category_subcategories",
                                                          x => new
                                                          {
                                                              x.category_id,
                                                              x.subcategory_id
                                                          });

                                         table.ForeignKey("FK_category_subcategories_categories_category_id",
                                                          x => x.category_id,
                                                          "categories",
                                                          "id",
                                                          onDelete: ReferentialAction.Cascade);

                                         table.ForeignKey("FK_category_subcategories_subcategories_subcategory_id",
                                                          x => x.subcategory_id,
                                                          "subcategories",
                                                          "id",
                                                          onDelete: ReferentialAction.Cascade);
                                     });

        migrationBuilder.CreateTable("products",
                                     table => new
                                     {
                                         id = table.Column<long>("bigint", nullable: false)
                                                   .Annotation("Npgsql:ValueGenerationStrategy",
                                                               NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                                         brand_id = table.Column<long>("bigint", nullable: false),
                                         subcategory_id = table.Column<long>("bigint", nullable: false),
                                         name = table.Column<string>("text", nullable: false)
                                     },
                                     constraints: table =>
                                     {
                                         table.PrimaryKey("PK_products", x => x.id);

                                         table.ForeignKey("FK_products_brands_brand_id",
                                                          x => x.brand_id,
                                                          "brands",
                                                          "id",
                                                          onDelete: ReferentialAction.Cascade);

                                         table.ForeignKey("FK_products_subcategories_subcategory_id",
                                                          x => x.subcategory_id,
                                                          "subcategories",
                                                          "id",
                                                          onDelete: ReferentialAction.Cascade);
                                     });

        migrationBuilder.CreateTable("users",
                                     table => new
                                     {
                                         id = table.Column<long>("bigint", nullable: false)
                                                   .Annotation("Npgsql:ValueGenerationStrategy",
                                                               NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                                         email = table.Column<string>("text", nullable: false),
                                         password = table.Column<string>("text", nullable: false),
                                         user_type_id = table.Column<long>("bigint", nullable: false)
                                     },
                                     constraints: table =>
                                     {
                                         table.PrimaryKey("PK_users", x => x.id);

                                         table.ForeignKey("FK_users_user_types_user_type_id",
                                                          x => x.user_type_id,
                                                          "user_types",
                                                          "id",
                                                          onDelete: ReferentialAction.Cascade);
                                     });

        migrationBuilder.CreateTable("images_urls",
                                     table => new
                                     {
                                         id = table.Column<long>("bigint", nullable: false)
                                                   .Annotation("Npgsql:ValueGenerationStrategy",
                                                               NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                                         url = table.Column<string>("text", nullable: false),
                                         product_color_id = table.Column<long>("bigint", nullable: false)
                                     },
                                     constraints: table =>
                                     {
                                         table.PrimaryKey("PK_images_urls", x => x.id);

                                         table.ForeignKey("FK_images_urls_product_colors_product_color_id",
                                                          x => x.product_color_id,
                                                          "product_colors",
                                                          "id",
                                                          onDelete: ReferentialAction.Cascade);
                                     });

        migrationBuilder.CreateTable("product_option",
                                     table => new
                                     {
                                         id = table.Column<long>("bigint", nullable: false)
                                                   .Annotation("Npgsql:ValueGenerationStrategy",
                                                               NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                                         product_id = table.Column<long>("bigint", nullable: false),
                                         product_color_id = table.Column<long>("bigint", nullable: false),
                                         size = table.Column<string>("text", nullable: false),
                                         quantity = table.Column<int>("integer", nullable: false),
                                         price = table.Column<decimal>("numeric", nullable: false)
                                     },
                                     constraints: table =>
                                     {
                                         table.PrimaryKey("PK_product_option", x => x.id);

                                         table.ForeignKey("FK_product_option_product_colors_product_color_id",
                                                          x => x.product_color_id,
                                                          "product_colors",
                                                          "id",
                                                          onDelete: ReferentialAction.Cascade);

                                         table.ForeignKey("FK_product_option_products_product_id",
                                                          x => x.product_id,
                                                          "products",
                                                          "id",
                                                          onDelete: ReferentialAction.Cascade);
                                     });

        migrationBuilder.CreateTable("customers_info",
                                     table => new
                                     {
                                         user_id = table.Column<long>("bigint", nullable: false),
                                         name = table.Column<string>("text", nullable: true),
                                         surname = table.Column<string>("text", nullable: true),
                                         patronymic = table.Column<string>("text", nullable: true),
                                         address = table.Column<string>("text", nullable: true),
                                         phone = table.Column<string>("text", nullable: true)
                                     },
                                     constraints: table =>
                                     {
                                         table.PrimaryKey("PK_customers_info", x => x.user_id);

                                         table.ForeignKey("FK_customers_info_users_user_id",
                                                          x => x.user_id,
                                                          "users",
                                                          "id",
                                                          onDelete: ReferentialAction.Cascade);
                                     });

        migrationBuilder.CreateTable("order",
                                     table => new
                                     {
                                         id = table.Column<long>("bigint", nullable: false)
                                                   .Annotation("Npgsql:ValueGenerationStrategy",
                                                               NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                                         user_id = table.Column<long>("bigint", nullable: false),
                                         order_status_id = table.Column<long>("bigint", nullable: false),
                                         date_time = table.Column<DateTime>("timestamp with time zone", nullable: false)
                                     },
                                     constraints: table =>
                                     {
                                         table.PrimaryKey("PK_order", x => x.id);

                                         table.ForeignKey("FK_order_order_statuses_order_status_id",
                                                          x => x.order_status_id,
                                                          "order_statuses",
                                                          "id",
                                                          onDelete: ReferentialAction.Cascade);

                                         table.ForeignKey("FK_order_users_user_id",
                                                          x => x.user_id,
                                                          "users",
                                                          "id",
                                                          onDelete: ReferentialAction.Cascade);
                                     });

        migrationBuilder.CreateTable("reviews",
                                     table => new
                                     {
                                         id = table.Column<long>("bigint", nullable: false)
                                                   .Annotation("Npgsql:ValueGenerationStrategy",
                                                               NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                                         rate = table.Column<int>("integer", nullable: false),
                                         date_time =
                                             table.Column<DateTime>("timestamp with time zone", nullable: false),
                                         comment = table.Column<string>("text", nullable: true),
                                         user_id = table.Column<long>("bigint", nullable: false),
                                         product_id = table.Column<long>("bigint", nullable: false)
                                     },
                                     constraints: table =>
                                     {
                                         table.PrimaryKey("PK_reviews", x => x.id);

                                         table.ForeignKey("FK_reviews_products_product_id",
                                                          x => x.product_id,
                                                          "products",
                                                          "id",
                                                          onDelete: ReferentialAction.Cascade);

                                         table.ForeignKey("FK_reviews_users_user_id",
                                                          x => x.user_id,
                                                          "users",
                                                          "id",
                                                          onDelete: ReferentialAction.Cascade);
                                     });

        migrationBuilder.CreateTable("orders_product_options",
                                     table => new
                                     {
                                         order_id = table.Column<long>("bigint", nullable: false),
                                         product_option_id = table.Column<long>("bigint", nullable: false),
                                         amount = table.Column<int>("integer", nullable: false)
                                     },
                                     constraints: table =>
                                     {
                                         table.PrimaryKey("PK_orders_product_options",
                                                          x => new
                                                          {
                                                              x.order_id,
                                                              x.product_option_id
                                                          });

                                         table.ForeignKey("FK_orders_product_options_order_order_id",
                                                          x => x.order_id,
                                                          "order",
                                                          "id",
                                                          onDelete: ReferentialAction.Cascade);

                                         table.ForeignKey("FK_orders_product_options_product_option_product_option_id",
                                                          x => x.product_option_id,
                                                          "product_option",
                                                          "id",
                                                          onDelete: ReferentialAction.Cascade);
                                     });

        migrationBuilder.CreateIndex("IX_category_subcategories_subcategory_id",
                                     "category_subcategories",
                                     "subcategory_id");

        migrationBuilder.CreateIndex("IX_images_urls_product_color_id",
                                     "images_urls",
                                     "product_color_id");

        migrationBuilder.CreateIndex("IX_order_order_status_id",
                                     "order",
                                     "order_status_id");

        migrationBuilder.CreateIndex("IX_order_user_id",
                                     "order",
                                     "user_id");

        migrationBuilder.CreateIndex("IX_orders_product_options_product_option_id",
                                     "orders_product_options",
                                     "product_option_id");

        migrationBuilder.CreateIndex("IX_product_option_product_color_id",
                                     "product_option",
                                     "product_color_id");

        migrationBuilder.CreateIndex("IX_product_option_product_id",
                                     "product_option",
                                     "product_id");

        migrationBuilder.CreateIndex("IX_products_brand_id",
                                     "products",
                                     "brand_id");

        migrationBuilder.CreateIndex("IX_products_subcategory_id",
                                     "products",
                                     "subcategory_id");

        migrationBuilder.CreateIndex("IX_reviews_product_id",
                                     "reviews",
                                     "product_id");

        migrationBuilder.CreateIndex("IX_reviews_user_id",
                                     "reviews",
                                     "user_id");

        migrationBuilder.CreateIndex("IX_sections_categories_section_id",
                                     "sections_categories",
                                     "section_id");

        migrationBuilder.CreateIndex("IX_users_user_type_id",
                                     "users",
                                     "user_type_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable("category_subcategories");

        migrationBuilder.DropTable("customers_info");

        migrationBuilder.DropTable("images_urls");

        migrationBuilder.DropTable("orders_product_options");

        migrationBuilder.DropTable("reviews");

        migrationBuilder.DropTable("sections_categories");

        migrationBuilder.DropTable("order");

        migrationBuilder.DropTable("product_option");

        migrationBuilder.DropTable("categories");

        migrationBuilder.DropTable("sections");

        migrationBuilder.DropTable("order_statuses");

        migrationBuilder.DropTable("users");

        migrationBuilder.DropTable("product_colors");

        migrationBuilder.DropTable("products");

        migrationBuilder.DropTable("user_types");

        migrationBuilder.DropTable("colors");

        migrationBuilder.DropTable("brands");

        migrationBuilder.DropTable("subcategories");
    }
}