#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infrastructure.Data.ShopContextMigrations;

/// <inheritdoc />
public partial class PrimaryKeyToCustomerInfoAdded : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropPrimaryKey
            ("PK_customers_info",
             "customers_info");

        migrationBuilder.AddColumn<long>
                            ("Id",
                             "customers_info",
                             "bigint",
                             nullable: false,
                             defaultValue: 0L)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

        migrationBuilder.AddPrimaryKey
            ("PK_customers_info",
             "customers_info",
             "Id");

        migrationBuilder.CreateIndex
            ("IX_customers_info_user_id",
             "customers_info",
             "user_id",
             unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropPrimaryKey
            ("PK_customers_info",
             "customers_info");

        migrationBuilder.DropIndex
            ("IX_customers_info_user_id",
             "customers_info");

        migrationBuilder.DropColumn
            ("Id",
             "customers_info");

        migrationBuilder.AddPrimaryKey
            ("PK_customers_info",
             "customers_info",
             "user_id");
    }
}