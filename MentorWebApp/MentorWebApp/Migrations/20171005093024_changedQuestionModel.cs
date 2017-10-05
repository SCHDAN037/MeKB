using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MentorWebApp.Migrations
{
    public partial class changedQuestionModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Replies_Questions_QuestionId",
                table: "Replies");

            migrationBuilder.DropIndex(
                name: "IX_Replies_QuestionId",
                table: "Replies");

            migrationBuilder.AlterColumn<string>(
                name: "QuestionId",
                table: "Replies",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NoOfReplies",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoOfReplies",
                table: "Questions");

            migrationBuilder.AlterColumn<string>(
                name: "QuestionId",
                table: "Replies",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Replies_QuestionId",
                table: "Replies",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_Questions_QuestionId",
                table: "Replies",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
