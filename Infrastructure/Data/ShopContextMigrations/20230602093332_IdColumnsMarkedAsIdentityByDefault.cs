#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infrastructure.Data.ShopContextMigrations;

/// <inheritdoc />
public partial class IdColumnsMarkedAsIdentityByDefault : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<long>("id",
                                           "users",
                                           "bigint",
                                           nullable: false,
                                           oldClrType: typeof(long),
                                           oldType: "bigint")
                        .Annotation("Npgsql:ValueGenerationStrategy",
                                    NpgsqlValueGenerationStrategy.IdentityAlwaysColumn)
                        .OldAnnotation("Npgsql:ValueGenerationStrategy",
                                       NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

        migrationBuilder.AlterColumn<long>("id",
                                           "user_types",
                                           "bigint",
                                           nullable: false,
                                           oldClrType: typeof(long),
                                           oldType: "bigint")
                        .Annotation("Npgsql:ValueGenerationStrategy",
                                    NpgsqlValueGenerationStrategy.IdentityAlwaysColumn)
                        .OldAnnotation("Npgsql:ValueGenerationStrategy",
                                       NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

        migrationBuilder.AlterColumn<long>("id",
                                           "subcategories",
                                           "bigint",
                                           nullable: false,
                                           oldClrType: typeof(long),
                                           oldType: "bigint")
                        .Annotation("Npgsql:ValueGenerationStrategy",
                                    NpgsqlValueGenerationStrategy.IdentityAlwaysColumn)
                        .OldAnnotation("Npgsql:ValueGenerationStrategy",
                                       NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

        migrationBuilder.AlterColumn<long>("id",
                                           "sections",
                                           "bigint",
                                           nullable: false,
                                           oldClrType: typeof(long),
                                           oldType: "bigint")
                        .Annotation("Npgsql:ValueGenerationStrategy",
                                    NpgsqlValueGenerationStrategy.IdentityAlwaysColumn)
                        .OldAnnotation("Npgsql:ValueGenerationStrategy",
                                       NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

        migrationBuilder.AlterColumn<long>("id",
                                           "reviews",
                                           "bigint",
                                           nullable: false,
                                           oldClrType: typeof(long),
                                           oldType: "bigint")
                        .Annotation("Npgsql:ValueGenerationStrategy",
                                    NpgsqlValueGenerationStrategy.IdentityAlwaysColumn)
                        .OldAnnotation("Npgsql:ValueGenerationStrategy",
                                       NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

        migrationBuilder.AlterColumn<long>("id",
                                           "products",
                                           "bigint",
                                           nullable: false,
                                           oldClrType: typeof(long),
                                           oldType: "bigint")
                        .Annotation("Npgsql:ValueGenerationStrategy",
                                    NpgsqlValueGenerationStrategy.IdentityAlwaysColumn)
                        .OldAnnotation("Npgsql:ValueGenerationStrategy",
                                       NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

        migrationBuilder.AlterColumn<long>("id",
                                           "product_option",
                                           "bigint",
                                           nullable: false,
                                           oldClrType: typeof(long),
                                           oldType: "bigint")
                        .Annotation("Npgsql:ValueGenerationStrategy",
                                    NpgsqlValueGenerationStrategy.IdentityAlwaysColumn)
                        .OldAnnotation("Npgsql:ValueGenerationStrategy",
                                       NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

        migrationBuilder.AlterColumn<long>("id",
                                           "order_statuses",
                                           "bigint",
                                           nullable: false,
                                           oldClrType: typeof(long),
                                           oldType: "bigint")
                        .Annotation("Npgsql:ValueGenerationStrategy",
                                    NpgsqlValueGenerationStrategy.IdentityAlwaysColumn)
                        .OldAnnotation("Npgsql:ValueGenerationStrategy",
                                       NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

        migrationBuilder.AlterColumn<long>("id",
                                           "order",
                                           "bigint",
                                           nullable: false,
                                           oldClrType: typeof(long),
                                           oldType: "bigint")
                        .Annotation("Npgsql:ValueGenerationStrategy",
                                    NpgsqlValueGenerationStrategy.IdentityAlwaysColumn)
                        .OldAnnotation("Npgsql:ValueGenerationStrategy",
                                       NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

        migrationBuilder.AlterColumn<long>("id",
                                           "images_urls",
                                           "bigint",
                                           nullable: false,
                                           oldClrType: typeof(long),
                                           oldType: "bigint")
                        .Annotation("Npgsql:ValueGenerationStrategy",
                                    NpgsqlValueGenerationStrategy.IdentityAlwaysColumn)
                        .OldAnnotation("Npgsql:ValueGenerationStrategy",
                                       NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

        migrationBuilder.AlterColumn<long>("Id",
                                           "customers_info",
                                           "bigint",
                                           nullable: false,
                                           oldClrType: typeof(long),
                                           oldType: "bigint")
                        .Annotation("Npgsql:ValueGenerationStrategy",
                                    NpgsqlValueGenerationStrategy.IdentityAlwaysColumn)
                        .OldAnnotation("Npgsql:ValueGenerationStrategy",
                                       NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

        migrationBuilder.AlterColumn<long>("id",
                                           "colors",
                                           "bigint",
                                           nullable: false,
                                           oldClrType: typeof(long),
                                           oldType: "bigint")
                        .Annotation("Npgsql:ValueGenerationStrategy",
                                    NpgsqlValueGenerationStrategy.IdentityAlwaysColumn)
                        .OldAnnotation("Npgsql:ValueGenerationStrategy",
                                       NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

        migrationBuilder.AlterColumn<long>("id",
                                           "categories",
                                           "bigint",
                                           nullable: false,
                                           oldClrType: typeof(long),
                                           oldType: "bigint")
                        .Annotation("Npgsql:ValueGenerationStrategy",
                                    NpgsqlValueGenerationStrategy.IdentityAlwaysColumn)
                        .OldAnnotation("Npgsql:ValueGenerationStrategy",
                                       NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

        migrationBuilder.AlterColumn<long>("id",
                                           "brands",
                                           "bigint",
                                           nullable: false,
                                           oldClrType: typeof(long),
                                           oldType: "bigint")
                        .Annotation("Npgsql:ValueGenerationStrategy",
                                    NpgsqlValueGenerationStrategy.IdentityAlwaysColumn)
                        .OldAnnotation("Npgsql:ValueGenerationStrategy",
                                       NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<long>("id",
                                           "users",
                                           "bigint",
                                           nullable: false,
                                           oldClrType: typeof(long),
                                           oldType: "bigint")
                        .Annotation("Npgsql:ValueGenerationStrategy",
                                    NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                        .OldAnnotation("Npgsql:ValueGenerationStrategy",
                                       NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

        migrationBuilder.AlterColumn<long>("id",
                                           "user_types",
                                           "bigint",
                                           nullable: false,
                                           oldClrType: typeof(long),
                                           oldType: "bigint")
                        .Annotation("Npgsql:ValueGenerationStrategy",
                                    NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                        .OldAnnotation("Npgsql:ValueGenerationStrategy",
                                       NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

        migrationBuilder.AlterColumn<long>("id",
                                           "subcategories",
                                           "bigint",
                                           nullable: false,
                                           oldClrType: typeof(long),
                                           oldType: "bigint")
                        .Annotation("Npgsql:ValueGenerationStrategy",
                                    NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                        .OldAnnotation("Npgsql:ValueGenerationStrategy",
                                       NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

        migrationBuilder.AlterColumn<long>("id",
                                           "sections",
                                           "bigint",
                                           nullable: false,
                                           oldClrType: typeof(long),
                                           oldType: "bigint")
                        .Annotation("Npgsql:ValueGenerationStrategy",
                                    NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                        .OldAnnotation("Npgsql:ValueGenerationStrategy",
                                       NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

        migrationBuilder.AlterColumn<long>("id",
                                           "reviews",
                                           "bigint",
                                           nullable: false,
                                           oldClrType: typeof(long),
                                           oldType: "bigint")
                        .Annotation("Npgsql:ValueGenerationStrategy",
                                    NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                        .OldAnnotation("Npgsql:ValueGenerationStrategy",
                                       NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

        migrationBuilder.AlterColumn<long>("id",
                                           "products",
                                           "bigint",
                                           nullable: false,
                                           oldClrType: typeof(long),
                                           oldType: "bigint")
                        .Annotation("Npgsql:ValueGenerationStrategy",
                                    NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                        .OldAnnotation("Npgsql:ValueGenerationStrategy",
                                       NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

        migrationBuilder.AlterColumn<long>("id",
                                           "product_option",
                                           "bigint",
                                           nullable: false,
                                           oldClrType: typeof(long),
                                           oldType: "bigint")
                        .Annotation("Npgsql:ValueGenerationStrategy",
                                    NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                        .OldAnnotation("Npgsql:ValueGenerationStrategy",
                                       NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

        migrationBuilder.AlterColumn<long>("id",
                                           "order_statuses",
                                           "bigint",
                                           nullable: false,
                                           oldClrType: typeof(long),
                                           oldType: "bigint")
                        .Annotation("Npgsql:ValueGenerationStrategy",
                                    NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                        .OldAnnotation("Npgsql:ValueGenerationStrategy",
                                       NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

        migrationBuilder.AlterColumn<long>("id",
                                           "order",
                                           "bigint",
                                           nullable: false,
                                           oldClrType: typeof(long),
                                           oldType: "bigint")
                        .Annotation("Npgsql:ValueGenerationStrategy",
                                    NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                        .OldAnnotation("Npgsql:ValueGenerationStrategy",
                                       NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

        migrationBuilder.AlterColumn<long>("id",
                                           "images_urls",
                                           "bigint",
                                           nullable: false,
                                           oldClrType: typeof(long),
                                           oldType: "bigint")
                        .Annotation("Npgsql:ValueGenerationStrategy",
                                    NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                        .OldAnnotation("Npgsql:ValueGenerationStrategy",
                                       NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

        migrationBuilder.AlterColumn<long>("Id",
                                           "customers_info",
                                           "bigint",
                                           nullable: false,
                                           oldClrType: typeof(long),
                                           oldType: "bigint")
                        .Annotation("Npgsql:ValueGenerationStrategy",
                                    NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                        .OldAnnotation("Npgsql:ValueGenerationStrategy",
                                       NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

        migrationBuilder.AlterColumn<long>("id",
                                           "colors",
                                           "bigint",
                                           nullable: false,
                                           oldClrType: typeof(long),
                                           oldType: "bigint")
                        .Annotation("Npgsql:ValueGenerationStrategy",
                                    NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                        .OldAnnotation("Npgsql:ValueGenerationStrategy",
                                       NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

        migrationBuilder.AlterColumn<long>("id",
                                           "categories",
                                           "bigint",
                                           nullable: false,
                                           oldClrType: typeof(long),
                                           oldType: "bigint")
                        .Annotation("Npgsql:ValueGenerationStrategy",
                                    NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                        .OldAnnotation("Npgsql:ValueGenerationStrategy",
                                       NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

        migrationBuilder.AlterColumn<long>("id",
                                           "brands",
                                           "bigint",
                                           nullable: false,
                                           oldClrType: typeof(long),
                                           oldType: "bigint")
                        .Annotation("Npgsql:ValueGenerationStrategy",
                                    NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                        .OldAnnotation("Npgsql:ValueGenerationStrategy",
                                       NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);
    }
}