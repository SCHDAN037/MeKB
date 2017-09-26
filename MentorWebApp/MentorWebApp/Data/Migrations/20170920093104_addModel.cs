using Microsoft.EntityFrameworkCore.Migrations;

namespace MentorWebApp.Data.Migrations
{
    public partial class addModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                "Enabled",
                "AspNetUsers",
                "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                "Role",
                "AspNetUsers",
                "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                "Uctid",
                "AspNetUsers",
                "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "Enabled",
                "AspNetUsers");

            migrationBuilder.DropColumn(
                "Role",
                "AspNetUsers");

            migrationBuilder.DropColumn(
                "Uctid",
                "AspNetUsers");
        }
    }
}