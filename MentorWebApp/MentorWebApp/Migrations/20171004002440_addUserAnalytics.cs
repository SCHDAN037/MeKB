using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MentorWebApp.Migrations
{
    public partial class addUserAnalytics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AnalyticNewIdentity",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserAnalytic",
                columns: table => new
                {
                    NewIdentity = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    LastLoginDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberOfQuestions = table.Column<int>(type: "int", nullable: false),
                    NumberOfReplies = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeekLoginCheckStringStore = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAnalytic", x => x.NewIdentity);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AnalyticNewIdentity",
                table: "AspNetUsers",
                column: "AnalyticNewIdentity");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserAnalytic_AnalyticNewIdentity",
                table: "AspNetUsers",
                column: "AnalyticNewIdentity",
                principalTable: "UserAnalytic",
                principalColumn: "NewIdentity",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserAnalytic_AnalyticNewIdentity",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "UserAnalytic");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AnalyticNewIdentity",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AnalyticNewIdentity",
                table: "AspNetUsers");
        }
    }
}
