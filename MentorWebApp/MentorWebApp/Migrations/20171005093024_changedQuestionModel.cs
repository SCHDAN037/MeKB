using Microsoft.EntityFrameworkCore.Migrations;

namespace MentorWebApp.Migrations
{
    public partial class changedQuestionModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Replies_Questions_QuestionId",
                "Replies");

            migrationBuilder.DropIndex(
                "IX_Replies_QuestionId",
                "Replies");

            migrationBuilder.AlterColumn<string>(
                "QuestionId",
                "Replies",
                "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                "NoOfReplies",
                "Questions",
                "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "NoOfReplies",
                "Questions");

            migrationBuilder.AlterColumn<string>(
                "QuestionId",
                "Replies",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                "IX_Replies_QuestionId",
                "Replies",
                "QuestionId");

            migrationBuilder.AddForeignKey(
                "FK_Replies_Questions_QuestionId",
                "Replies",
                "QuestionId",
                "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}