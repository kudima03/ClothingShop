#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.ShopContextMigrations;

/// <inheritdoc />
public partial class CreationDateTimePropertyAdded : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<DateTime>
            ("creation_date_time",
             "shopping_carts_product_options",
             "timestamp with time zone",
             nullable: false,
             defaultValue: new DateTime
                 (1,
                  1,
                  1,
                  0,
                  0,
                  0,
                  0,
                  DateTimeKind.Unspecified));
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn
            ("creation_date_time",
             "shopping_carts_product_options");
    }
}