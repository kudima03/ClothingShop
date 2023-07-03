#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.ShopContextMigrations;

/// <inheritdoc />
public partial class DeletionDateTimeAddedToUser : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<DateTime>
            ("deletion_date_time",
             "users",
             "timestamp with time zone",
             nullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn
            ("deletion_date_time",
             "users");
    }
}