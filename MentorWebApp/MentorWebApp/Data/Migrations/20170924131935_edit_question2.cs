using Microsoft.EntityFrameworkCore.Migrations;

namespace MentorWebApp.Data.Migrations
{
    public partial class edit_question2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "Title",
                "Replies");

            migrationBuilder.AddColumn<string>(
                "Tags",
                "Questions",
                "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "Tags",
                "Questions");

            migrationBuilder.AddColumn<string>(
                "Title",
                "Replies",
                nullable: true);
        }
    }
}