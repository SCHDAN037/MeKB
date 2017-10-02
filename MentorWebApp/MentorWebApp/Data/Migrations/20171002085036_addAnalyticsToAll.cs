using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MentorWebApp.Data.Migrations
{
    public partial class addAnalyticsToAll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SearchResults_Analytic_AnalyticId",
                table: "SearchResults");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Analytic",
                table: "Analytic");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Analytic");

            migrationBuilder.DropColumn(
                name: "Clicks",
                table: "Analytic");

            migrationBuilder.DropColumn(
                name: "Helpful",
                table: "Analytic");

            migrationBuilder.DropColumn(
                name: "UnHelpful",
                table: "Analytic");

            migrationBuilder.RenameTable(
                name: "Analytic",
                newName: "SearchAnalytics");

            migrationBuilder.AddColumn<int>(
                name: "AnalyticId",
                table: "Resources",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AnalyticId",
                table: "Replies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AnalyticId",
                table: "Questions",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SucceedClicks",
                table: "SearchAnalytics",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NoResultsCount",
                table: "SearchAnalytics",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NoOfResults",
                table: "SearchAnalytics",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SearchAnalytics",
                table: "SearchAnalytics",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ContentAnalytics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Clicks = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    Helpful = table.Column<int>(type: "int", nullable: false),
                    ObjectId = table.Column<int>(type: "int", nullable: false),
                    UnHelpful = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentAnalytics", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Resources_AnalyticId",
                table: "Resources",
                column: "AnalyticId");

            migrationBuilder.CreateIndex(
                name: "IX_Replies_AnalyticId",
                table: "Replies",
                column: "AnalyticId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_AnalyticId",
                table: "Questions",
                column: "AnalyticId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_ContentAnalytics_AnalyticId",
                table: "Questions",
                column: "AnalyticId",
                principalTable: "ContentAnalytics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_ContentAnalytics_AnalyticId",
                table: "Replies",
                column: "AnalyticId",
                principalTable: "ContentAnalytics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_ContentAnalytics_AnalyticId",
                table: "Resources",
                column: "AnalyticId",
                principalTable: "ContentAnalytics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SearchResults_SearchAnalytics_AnalyticId",
                table: "SearchResults",
                column: "AnalyticId",
                principalTable: "SearchAnalytics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_ContentAnalytics_AnalyticId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_Replies_ContentAnalytics_AnalyticId",
                table: "Replies");

            migrationBuilder.DropForeignKey(
                name: "FK_Resources_ContentAnalytics_AnalyticId",
                table: "Resources");

            migrationBuilder.DropForeignKey(
                name: "FK_SearchResults_SearchAnalytics_AnalyticId",
                table: "SearchResults");

            migrationBuilder.DropTable(
                name: "ContentAnalytics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SearchAnalytics",
                table: "SearchAnalytics");

            migrationBuilder.DropIndex(
                name: "IX_Resources_AnalyticId",
                table: "Resources");

            migrationBuilder.DropIndex(
                name: "IX_Replies_AnalyticId",
                table: "Replies");

            migrationBuilder.DropIndex(
                name: "IX_Questions_AnalyticId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "AnalyticId",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "AnalyticId",
                table: "Replies");

            migrationBuilder.DropColumn(
                name: "AnalyticId",
                table: "Questions");

            migrationBuilder.RenameTable(
                name: "SearchAnalytics",
                newName: "Analytic");

            migrationBuilder.AlterColumn<int>(
                name: "SucceedClicks",
                table: "Analytic",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NoResultsCount",
                table: "Analytic",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NoOfResults",
                table: "Analytic",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Analytic",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Clicks",
                table: "Analytic",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Helpful",
                table: "Analytic",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnHelpful",
                table: "Analytic",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Analytic",
                table: "Analytic",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SearchResults_Analytic_AnalyticId",
                table: "SearchResults",
                column: "AnalyticId",
                principalTable: "Analytic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
