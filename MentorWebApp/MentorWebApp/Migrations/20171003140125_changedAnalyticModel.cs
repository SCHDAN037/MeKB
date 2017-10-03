using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MentorWebApp.Migrations
{
    public partial class changedAnalyticModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ObjectId",
                table: "SearchAnalytics");

            migrationBuilder.DropColumn(
                name: "ObjectId",
                table: "ContentAnalytics");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ObjectId",
                table: "SearchAnalytics",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ObjectId",
                table: "ContentAnalytics",
                nullable: true);
        }
    }
}
