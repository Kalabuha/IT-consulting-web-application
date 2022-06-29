using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbContextProfi.Migrations
{
    public partial class AddDateReceiptApplicationColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date_receipt_application",
                table: "Guests_applications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date_receipt_application",
                table: "Guests_applications");
        }
    }
}
