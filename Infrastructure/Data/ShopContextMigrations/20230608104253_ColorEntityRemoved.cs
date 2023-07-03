#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infrastructure.Data.ShopContextMigrations;

/// <inheritdoc />
public partial class ColorEntityRemoved : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey
            ("FK_product_colors_colors_id",
             "product_colors");

        migrationBuilder.DropTable("colors");

        migrationBuilder.DropColumn
            ("color_id",
             "product_colors");

        migrationBuilder.AlterColumn<long>
                            ("id",
                             "product_colors",
                             "bigint",
                             nullable: false,
                             oldClrType: typeof(long),
                             oldType: "bigint")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

        migrationBuilder.AddColumn<string>
            ("color_hex",
             "product_colors",
             "text",
             nullable: false,
             defaultValue: "");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn
            ("color_hex",
             "product_colors");

        migrationBuilder.AlterColumn<long>
                            ("id",
                             "product_colors",
                             "bigint",
                             nullable: false,
                             oldClrType: typeof(long),
                             oldType: "bigint")
                        .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

        migrationBuilder.AddColumn<long>
            ("color_id",
             "product_colors",
             "bigint",
             nullable: false,
             defaultValue: 0L);

        migrationBuilder.CreateTable
            ("colors",
             table => new
             {
                 id = table.Column<long>("bigint", nullable: false)
                           .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                 hex = table.Column<string>("text", nullable: false),
                 name = table.Column<string>("text", nullable: false)
             },
             constraints: table =>
             {
                 table.PrimaryKey("PK_colors", x => x.id);
             });

        migrationBuilder.AddForeignKey
            ("FK_product_colors_colors_id",
             "product_colors",
             "id",
             "colors",
             principalColumn: "id",
             onDelete: ReferentialAction.Cascade);
    }
}