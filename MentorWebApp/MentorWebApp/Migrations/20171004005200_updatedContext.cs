using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MentorWebApp.Migrations
{
    public partial class updatedContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserAnalytic_AnalyticNewIdentity",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAnalytic",
                table: "UserAnalytic");

            migrationBuilder.RenameTable(
                name: "UserAnalytic",
                newName: "UserAnalytics");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAnalytics",
                table: "UserAnalytics",
                column: "NewIdentity");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserAnalytics_AnalyticNewIdentity",
                table: "AspNetUsers",
                column: "AnalyticNewIdentity",
                principalTable: "UserAnalytics",
                principalColumn: "NewIdentity",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserAnalytics_AnalyticNewIdentity",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAnalytics",
                table: "UserAnalytics");

            migrationBuilder.RenameTable(
                name: "UserAnalytics",
                newName: "UserAnalytic");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAnalytic",
                table: "UserAnalytic",
                column: "NewIdentity");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserAnalytic_AnalyticNewIdentity",
                table: "AspNetUsers",
                column: "AnalyticNewIdentity",
                principalTable: "UserAnalytic",
                principalColumn: "NewIdentity",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
