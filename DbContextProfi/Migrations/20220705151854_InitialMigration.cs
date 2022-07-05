using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbContextProfi.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Creation_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Short_description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Long_description = table.Column<string>(type: "nvarchar(max)", maxLength: 6000, nullable: false),
                    Blog_image = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Is_published = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Postcode = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Fax = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Map = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Is_posted_on_the_page = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Guests_applications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Application_number = table.Column<int>(type: "int", nullable: false),
                    Guest_name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Guest_email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Guests_application_text = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    Date_receipt_application = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Application_processing_status = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guests_applications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Home_page_images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Is_posted_on_the_page = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Home_page_images", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Home_page_text",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    Is_posted_on_the_page = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Home_page_text", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Customer_company_logo = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Link_to_customer_site = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Project_article = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Is_published = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Service_description = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Is_published = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Guests_applications");

            migrationBuilder.DropTable(
                name: "Home_page_images");

            migrationBuilder.DropTable(
                name: "Home_page_text");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Services");
        }
    }
}
