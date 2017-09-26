using Microsoft.EntityFrameworkCore.Migrations;

namespace MentorWebApp.Data.Migrations
{
    public partial class edit_question1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                "Title",
                "Replies",
                "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "Title",
                "Replies");
        }
    }
}