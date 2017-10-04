﻿// <auto-generated />
using MentorWebApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace MentorWebApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MentorWebApp.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("AnalyticNewIdentity");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("Enabled");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("Permissions");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UctNumber");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("AnalyticNewIdentity");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("MentorWebApp.Models.ContentAnalytic", b =>
                {
                    b.Property<string>("NewIdentity")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Clicks");

                    b.Property<string>("ContentId");

                    b.Property<int>("Count");

                    b.Property<int>("Helpful");

                    b.Property<int>("UnHelpful");

                    b.HasKey("NewIdentity");

                    b.ToTable("ContentAnalytics");
                });

            modelBuilder.Entity("MentorWebApp.Models.Question", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AnalyticNewIdentity");

                    b.Property<bool>("Anonymous");

                    b.Property<string>("ApplicationUserId");

                    b.Property<DateTime>("DatePosted");

                    b.Property<string>("MessageContent");

                    b.Property<string>("Tags");

                    b.Property<string>("Title");

                    b.Property<string>("UctNumber");

                    b.HasKey("Id");

                    b.HasIndex("AnalyticNewIdentity");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("MentorWebApp.Models.Reply", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AnalyticNewIdentity");

                    b.Property<string>("ApplicationUserId");

                    b.Property<DateTime>("DatePosted");

                    b.Property<string>("MessageContent");

                    b.Property<string>("QuestionId");

                    b.Property<string>("UctNumber");

                    b.HasKey("Id");

                    b.HasIndex("AnalyticNewIdentity");

                    b.HasIndex("QuestionId");

                    b.ToTable("Replies");
                });

            modelBuilder.Entity("MentorWebApp.Models.Resource", b =>
                {
                    b.Property<string>("ResourceId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AnalyticNewIdentity");

                    b.Property<string>("ApplicationUserId");

                    b.Property<DateTime>("DateAdded");

                    b.Property<string>("Link");

                    b.Property<string>("Tags");

                    b.Property<string>("Title");

                    b.Property<string>("Type");

                    b.Property<string>("UctNumber");

                    b.HasKey("ResourceId");

                    b.HasIndex("AnalyticNewIdentity");

                    b.ToTable("Resources");
                });

            modelBuilder.Entity("MentorWebApp.Models.SearchAnalytic", b =>
                {
                    b.Property<string>("NewIdentity")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Count");

                    b.Property<int>("NoOfResults");

                    b.Property<int>("NoResultsCount");

                    b.Property<string>("SearchResultId");

                    b.Property<int>("SucceedClicks");

                    b.HasKey("NewIdentity");

                    b.HasIndex("SearchResultId")
                        .IsUnique()
                        .HasFilter("[SearchResultId] IS NOT NULL");

                    b.ToTable("SearchAnalytics");
                });

            modelBuilder.Entity("MentorWebApp.Models.SearchResult", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("NoOfResults");

                    b.Property<string>("searchVal");

                    b.Property<string>("sortVal");

                    b.Property<string>("typeVal");

                    b.HasKey("Id");

                    b.ToTable("SearchResults");
                });

            modelBuilder.Entity("MentorWebApp.Models.UserAnalytic", b =>
                {
                    b.Property<string>("NewIdentity")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Count");

                    b.Property<DateTime>("LastLoginDate");

                    b.Property<int>("NumberOfQuestions");

                    b.Property<int>("NumberOfReplies");

                    b.Property<string>("UserId");

                    b.Property<string>("WeekLoginCheckStringStore");

                    b.HasKey("NewIdentity");

                    b.ToTable("UserAnalytics");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("MentorWebApp.Models.ApplicationUser", b =>
                {
                    b.HasOne("MentorWebApp.Models.UserAnalytic", "Analytic")
                        .WithMany()
                        .HasForeignKey("AnalyticNewIdentity");
                });

            modelBuilder.Entity("MentorWebApp.Models.Question", b =>
                {
                    b.HasOne("MentorWebApp.Models.ContentAnalytic", "Analytic")
                        .WithMany()
                        .HasForeignKey("AnalyticNewIdentity");
                });

            modelBuilder.Entity("MentorWebApp.Models.Reply", b =>
                {
                    b.HasOne("MentorWebApp.Models.ContentAnalytic", "Analytic")
                        .WithMany()
                        .HasForeignKey("AnalyticNewIdentity");

                    b.HasOne("MentorWebApp.Models.Question")
                        .WithMany("Replies")
                        .HasForeignKey("QuestionId");
                });

            modelBuilder.Entity("MentorWebApp.Models.Resource", b =>
                {
                    b.HasOne("MentorWebApp.Models.ContentAnalytic", "Analytic")
                        .WithMany()
                        .HasForeignKey("AnalyticNewIdentity");
                });

            modelBuilder.Entity("MentorWebApp.Models.SearchAnalytic", b =>
                {
                    b.HasOne("MentorWebApp.Models.SearchResult")
                        .WithOne("Analytic")
                        .HasForeignKey("MentorWebApp.Models.SearchAnalytic", "SearchResultId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("MentorWebApp.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("MentorWebApp.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MentorWebApp.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("MentorWebApp.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}