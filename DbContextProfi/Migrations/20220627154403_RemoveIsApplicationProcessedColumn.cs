using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbContextProfi.Migrations
{
    public partial class RemoveIsApplicationProcessedColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Is_application_processed",
                table: "Guests_applications");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Is_application_processed",
                table: "Guests_applications",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
