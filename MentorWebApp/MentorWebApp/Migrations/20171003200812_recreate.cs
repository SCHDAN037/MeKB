using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MentorWebApp.Migrations
{
    public partial class recreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "AspNetRoles",
                table => new
                {
                    Id = table.Column<string>("nvarchar(450)", nullable: false),
                    ConcurrencyStamp = table.Column<string>("nvarchar(max)", nullable: true),
                    Name = table.Column<string>("nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>("nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_AspNetRoles", x => x.Id); });

            migrationBuilder.CreateTable(
                "AspNetUsers",
                table => new
                {
                    Id = table.Column<string>("nvarchar(450)", nullable: false),
                    AccessFailedCount = table.Column<int>("int", nullable: false),
                    ConcurrencyStamp = table.Column<string>("nvarchar(max)", nullable: true),
                    Email = table.Column<string>("nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>("bit", nullable: false),
                    Enabled = table.Column<bool>("bit", nullable: false),
                    LockoutEnabled = table.Column<bool>("bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>("datetimeoffset", nullable: true),
                    NormalizedEmail = table.Column<string>("nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>("nvarchar(256)", maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>("nvarchar(max)", nullable: true),
                    Permissions = table.Column<string>("nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>("nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>("bit", nullable: false),
                    SecurityStamp = table.Column<string>("nvarchar(max)", nullable: true),
                    TwoFactorEnabled = table.Column<bool>("bit", nullable: false),
                    UctNumber = table.Column<string>("nvarchar(max)", nullable: true),
                    UserName = table.Column<string>("nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_AspNetUsers", x => x.Id); });

            migrationBuilder.CreateTable(
                "ContentAnalytics",
                table => new
                {
                    NewIdentity = table.Column<string>("nvarchar(450)", nullable: false),
                    Clicks = table.Column<int>("int", nullable: false),
                    ContentId = table.Column<string>("nvarchar(max)", nullable: true),
                    Count = table.Column<int>("int", nullable: false),
                    Helpful = table.Column<int>("int", nullable: false),
                    UnHelpful = table.Column<int>("int", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_ContentAnalytics", x => x.NewIdentity); });

            migrationBuilder.CreateTable(
                "SearchResults",
                table => new
                {
                    Id = table.Column<string>("nvarchar(450)", nullable: false),
                    NoOfResults = table.Column<int>("int", nullable: false),
                    searchVal = table.Column<string>("nvarchar(max)", nullable: true),
                    sortVal = table.Column<string>("nvarchar(max)", nullable: true),
                    typeVal = table.Column<string>("nvarchar(max)", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_SearchResults", x => x.Id); });

            migrationBuilder.CreateTable(
                "AspNetRoleClaims",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>("nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>("nvarchar(max)", nullable: true),
                    RoleId = table.Column<string>("nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        x => x.RoleId,
                        "AspNetRoles",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "AspNetUserClaims",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>("nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>("nvarchar(max)", nullable: true),
                    UserId = table.Column<string>("nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        "FK_AspNetUserClaims_AspNetUsers_UserId",
                        x => x.UserId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "AspNetUserLogins",
                table => new
                {
                    LoginProvider = table.Column<string>("nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>("nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>("nvarchar(max)", nullable: true),
                    UserId = table.Column<string>("nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new {x.LoginProvider, x.ProviderKey});
                    table.ForeignKey(
                        "FK_AspNetUserLogins_AspNetUsers_UserId",
                        x => x.UserId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "AspNetUserRoles",
                table => new
                {
                    UserId = table.Column<string>("nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>("nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new {x.UserId, x.RoleId});
                    table.ForeignKey(
                        "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        x => x.RoleId,
                        "AspNetRoles",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_AspNetUserRoles_AspNetUsers_UserId",
                        x => x.UserId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "AspNetUserTokens",
                table => new
                {
                    UserId = table.Column<string>("nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>("nvarchar(450)", nullable: false),
                    Name = table.Column<string>("nvarchar(450)", nullable: false),
                    Value = table.Column<string>("nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new {x.UserId, x.LoginProvider, x.Name});
                    table.ForeignKey(
                        "FK_AspNetUserTokens_AspNetUsers_UserId",
                        x => x.UserId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Questions",
                table => new
                {
                    Id = table.Column<string>("nvarchar(450)", nullable: false),
                    AnalyticNewIdentity = table.Column<string>("nvarchar(450)", nullable: true),
                    Anonymous = table.Column<bool>("bit", nullable: false),
                    ApplicationUserId = table.Column<string>("nvarchar(max)", nullable: true),
                    DatePosted = table.Column<DateTime>("datetime2", nullable: false),
                    MessageContent = table.Column<string>("nvarchar(max)", nullable: true),
                    Tags = table.Column<string>("nvarchar(max)", nullable: true),
                    Title = table.Column<string>("nvarchar(max)", nullable: true),
                    UctNumber = table.Column<string>("nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        "FK_Questions_ContentAnalytics_AnalyticNewIdentity",
                        x => x.AnalyticNewIdentity,
                        "ContentAnalytics",
                        "NewIdentity",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "Resources",
                table => new
                {
                    ResourceId = table.Column<string>("nvarchar(450)", nullable: false),
                    AnalyticNewIdentity = table.Column<string>("nvarchar(450)", nullable: true),
                    ApplicationUserId = table.Column<string>("nvarchar(max)", nullable: true),
                    DateAdded = table.Column<DateTime>("datetime2", nullable: false),
                    Link = table.Column<string>("nvarchar(max)", nullable: true),
                    Tags = table.Column<string>("nvarchar(max)", nullable: true),
                    Title = table.Column<string>("nvarchar(max)", nullable: true),
                    Type = table.Column<string>("nvarchar(max)", nullable: true),
                    UctNumber = table.Column<string>("nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.ResourceId);
                    table.ForeignKey(
                        "FK_Resources_ContentAnalytics_AnalyticNewIdentity",
                        x => x.AnalyticNewIdentity,
                        "ContentAnalytics",
                        "NewIdentity",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "SearchAnalytics",
                table => new
                {
                    NewIdentity = table.Column<string>("nvarchar(450)", nullable: false),
                    Count = table.Column<int>("int", nullable: false),
                    NoOfResults = table.Column<int>("int", nullable: false),
                    NoResultsCount = table.Column<int>("int", nullable: false),
                    SearchResultId = table.Column<string>("nvarchar(450)", nullable: true),
                    SucceedClicks = table.Column<int>("int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchAnalytics", x => x.NewIdentity);
                    table.ForeignKey(
                        "FK_SearchAnalytics_SearchResults_SearchResultId",
                        x => x.SearchResultId,
                        "SearchResults",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "Replies",
                table => new
                {
                    Id = table.Column<string>("nvarchar(450)", nullable: false),
                    AnalyticNewIdentity = table.Column<string>("nvarchar(450)", nullable: true),
                    ApplicationUserId = table.Column<string>("nvarchar(max)", nullable: true),
                    DatePosted = table.Column<DateTime>("datetime2", nullable: false),
                    MessageContent = table.Column<string>("nvarchar(max)", nullable: true),
                    QuestionId = table.Column<string>("nvarchar(450)", nullable: true),
                    UctNumber = table.Column<string>("nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Replies", x => x.Id);
                    table.ForeignKey(
                        "FK_Replies_ContentAnalytics_AnalyticNewIdentity",
                        x => x.AnalyticNewIdentity,
                        "ContentAnalytics",
                        "NewIdentity",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_Replies_Questions_QuestionId",
                        x => x.QuestionId,
                        "Questions",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_AspNetRoleClaims_RoleId",
                "AspNetRoleClaims",
                "RoleId");

            migrationBuilder.CreateIndex(
                "RoleNameIndex",
                "AspNetRoles",
                "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                "IX_AspNetUserClaims_UserId",
                "AspNetUserClaims",
                "UserId");

            migrationBuilder.CreateIndex(
                "IX_AspNetUserLogins_UserId",
                "AspNetUserLogins",
                "UserId");

            migrationBuilder.CreateIndex(
                "IX_AspNetUserRoles_RoleId",
                "AspNetUserRoles",
                "RoleId");

            migrationBuilder.CreateIndex(
                "EmailIndex",
                "AspNetUsers",
                "NormalizedEmail");

            migrationBuilder.CreateIndex(
                "UserNameIndex",
                "AspNetUsers",
                "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                "IX_Questions_AnalyticNewIdentity",
                "Questions",
                "AnalyticNewIdentity");

            migrationBuilder.CreateIndex(
                "IX_Replies_AnalyticNewIdentity",
                "Replies",
                "AnalyticNewIdentity");

            migrationBuilder.CreateIndex(
                "IX_Replies_QuestionId",
                "Replies",
                "QuestionId");

            migrationBuilder.CreateIndex(
                "IX_Resources_AnalyticNewIdentity",
                "Resources",
                "AnalyticNewIdentity");

            migrationBuilder.CreateIndex(
                "IX_SearchAnalytics_SearchResultId",
                "SearchAnalytics",
                "SearchResultId",
                unique: true,
                filter: "[SearchResultId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "AspNetRoleClaims");

            migrationBuilder.DropTable(
                "AspNetUserClaims");

            migrationBuilder.DropTable(
                "AspNetUserLogins");

            migrationBuilder.DropTable(
                "AspNetUserRoles");

            migrationBuilder.DropTable(
                "AspNetUserTokens");

            migrationBuilder.DropTable(
                "Replies");

            migrationBuilder.DropTable(
                "Resources");

            migrationBuilder.DropTable(
                "SearchAnalytics");

            migrationBuilder.DropTable(
                "AspNetRoles");

            migrationBuilder.DropTable(
                "AspNetUsers");

            migrationBuilder.DropTable(
                "Questions");

            migrationBuilder.DropTable(
                "SearchResults");

            migrationBuilder.DropTable(
                "ContentAnalytics");
        }
    }
}