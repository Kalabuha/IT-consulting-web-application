using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbContextProfi.Migrations
{
    public partial class AddApplicationProcessingStatusColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Application_processing_status",
                table: "Guests_applications",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Application_processing_status",
                table: "Guests_applications");
        }
    }
}
