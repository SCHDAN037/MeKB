using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MentorWebApp.Data.Migrations
{
    public partial class addModelAgainTake2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Resources",
                table => new
                {
                    Id = table.Column<string>("nvarchar(450)", nullable: false),
                    DateAdded = table.Column<DateTime>("datetime2", nullable: false),
                    Tags = table.Column<string>("nvarchar(max)", nullable: true),
                    Title = table.Column<string>("nvarchar(max)", nullable: true),
                    Type = table.Column<string>("nvarchar(max)", nullable: true),
                    UserId = table.Column<string>("nvarchar(max)", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Resources", x => x.Id); });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Resources");
        }
    }
}