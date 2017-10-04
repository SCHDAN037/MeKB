using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MentorWebApp.Migrations
{
    public partial class addUserAnalytics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                "AnalyticNewIdentity",
                "AspNetUsers",
                "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                "UserAnalytic",
                table => new
                {
                    NewIdentity = table.Column<string>("nvarchar(450)", nullable: false),
                    Count = table.Column<int>("int", nullable: false),
                    LastLoginDate = table.Column<DateTime>("datetime2", nullable: false),
                    NumberOfQuestions = table.Column<int>("int", nullable: false),
                    NumberOfReplies = table.Column<int>("int", nullable: false),
                    UserId = table.Column<string>("nvarchar(max)", nullable: true),
                    WeekLoginCheckStringStore = table.Column<string>("nvarchar(max)", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_UserAnalytic", x => x.NewIdentity); });

            migrationBuilder.CreateIndex(
                "IX_AspNetUsers_AnalyticNewIdentity",
                "AspNetUsers",
                "AnalyticNewIdentity");

            migrationBuilder.AddForeignKey(
                "FK_AspNetUsers_UserAnalytic_AnalyticNewIdentity",
                "AspNetUsers",
                "AnalyticNewIdentity",
                "UserAnalytic",
                principalColumn: "NewIdentity",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_AspNetUsers_UserAnalytic_AnalyticNewIdentity",
                "AspNetUsers");

            migrationBuilder.DropTable(
                "UserAnalytic");

            migrationBuilder.DropIndex(
                "IX_AspNetUsers_AnalyticNewIdentity",
                "AspNetUsers");

            migrationBuilder.DropColumn(
                "AnalyticNewIdentity",
                "AspNetUsers");
        }
    }
}