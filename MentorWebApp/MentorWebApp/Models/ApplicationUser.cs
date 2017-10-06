using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

/**
 * 
 * .Net generated some of this code
 * 
 * 
 */
namespace MentorWebApp.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
        }

        //constructor used to help seed data
        public ApplicationUser(string uctNumber, string permissions, string email, string username, string password)
        {
            UctNumber = uctNumber;
            Enabled = true;
            Permissions = permissions;
            Email = email;
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            var hashed = passwordHasher.HashPassword(this, password);
            PasswordHash = hashed;
            UserName = username;
            Analytic = new UserAnalytic(Id);
        }


        public string UctNumber { get; set; }

        public string Permissions { get; set; }

        //if the user is enabled they can login 
        public bool Enabled { get; set; }

        //analytic object
        public UserAnalytic Analytic { get; set; }

        //needed to change this users role
        public async Task<bool> ChangeRoleAsync(string role, string old, UserManager<ApplicationUser> userManager)
        {
            var result = await userManager.RemoveFromRoleAsync(this, old);
            if (!result.Succeeded) return false;

            var addRes = await userManager.AddToRoleAsync(this, role);
            if (!addRes.Succeeded) return false;

            var update = await userManager.UpdateAsync(this);
            return update.Succeeded;
        }

        //a get for analytics
        public UserAnalytic GetAnalytic()
        {
            if (Analytic != null)
                return Analytic;
            Analytic = new UserAnalytic(Id);
            return Analytic;
        }
    }
}