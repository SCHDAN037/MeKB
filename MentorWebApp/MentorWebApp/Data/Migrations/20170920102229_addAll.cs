using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MentorWebApp.Data.Migrations
{
    public partial class addAll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "Uctid",
                "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                "UctiId",
                "AspNetUsers",
                "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                "Questions",
                table => new
                {
                    Id = table.Column<string>("nvarchar(450)", nullable: false),
                    Anonymous = table.Column<bool>("bit", nullable: false),
                    DatePosted = table.Column<DateTime>("datetime2", nullable: false),
                    MessageContent = table.Column<string>("nvarchar(max)", nullable: true),
                    UserId = table.Column<string>("nvarchar(max)", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Questions", x => x.Id); });

            migrationBuilder.CreateTable(
                "Replies",
                table => new
                {
                    Id = table.Column<string>("nvarchar(450)", nullable: false),
                    DatePosted = table.Column<DateTime>("datetime2", nullable: false),
                    MessageContent = table.Column<string>("nvarchar(max)", nullable: true),
                    QuestionId = table.Column<string>("nvarchar(max)", nullable: true),
                    UserId = table.Column<string>("nvarchar(max)", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Replies", x => x.Id); });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Questions");

            migrationBuilder.DropTable(
                "Replies");

            migrationBuilder.DropColumn(
                "UctiId",
                "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                "Uctid",
                "AspNetUsers",
                nullable: true);
        }
    }
}