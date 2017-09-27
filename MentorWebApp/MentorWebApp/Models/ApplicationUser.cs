using Microsoft.AspNetCore.Identity;

namespace MentorWebApp.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string UctiId { get; set; }
        public string Role { get; set; }
        public bool Enabled { get; set; }
        //public IdentityRole Role = new IdentityRole("Administrator");
    }
}