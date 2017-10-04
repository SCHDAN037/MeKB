using Microsoft.EntityFrameworkCore.Migrations;

namespace MentorWebApp.Migrations
{
    public partial class updatedContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_AspNetUsers_UserAnalytic_AnalyticNewIdentity",
                "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                "PK_UserAnalytic",
                "UserAnalytic");

            migrationBuilder.RenameTable(
                "UserAnalytic",
                newName: "UserAnalytics");

            migrationBuilder.AddPrimaryKey(
                "PK_UserAnalytics",
                "UserAnalytics",
                "NewIdentity");

            migrationBuilder.AddForeignKey(
                "FK_AspNetUsers_UserAnalytics_AnalyticNewIdentity",
                "AspNetUsers",
                "AnalyticNewIdentity",
                "UserAnalytics",
                principalColumn: "NewIdentity",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_AspNetUsers_UserAnalytics_AnalyticNewIdentity",
                "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                "PK_UserAnalytics",
                "UserAnalytics");

            migrationBuilder.RenameTable(
                "UserAnalytics",
                newName: "UserAnalytic");

            migrationBuilder.AddPrimaryKey(
                "PK_UserAnalytic",
                "UserAnalytic",
                "NewIdentity");

            migrationBuilder.AddForeignKey(
                "FK_AspNetUsers_UserAnalytic_AnalyticNewIdentity",
                "AspNetUsers",
                "AnalyticNewIdentity",
                "UserAnalytic",
                principalColumn: "NewIdentity",
                onDelete: ReferentialAction.Restrict);
        }
    }
}