using Microsoft.EntityFrameworkCore.Migrations;

namespace MentorWebApp.Data.Migrations
{
    public partial class editResource : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                "Link",
                "Resources",
                "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "Link",
                "Resources");
        }
    }
}