using Microsoft.AspNetCore.Identity;

namespace MentorWebApp.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
        }

        public ApplicationUser(string uctID, bool enabled, string email, string username, string password)
        {
            UctiId = uctID;
            Enabled = enabled;
            Email = email;
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            var hashed = passwordHasher.HashPassword(this, password);
            PasswordHash = hashed;
            UserName = username;
        }

        public string UctiId { get; set; }

        public bool Enabled { get; set; }
    }
}