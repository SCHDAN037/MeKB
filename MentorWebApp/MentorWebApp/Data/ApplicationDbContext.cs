using MentorWebApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MentorWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        public DbSet<Resource> Resources { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<SearchResult> SearchResults { get; set; }
        public DbSet<SearchAnalytic> SearchAnalytics { get; set; }
        public DbSet<ContentAnalytic> ContentAnalytics { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)

        {
            base.OnModelCreating(builder);
        }
    }
}