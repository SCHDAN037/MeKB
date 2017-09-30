using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace MentorWebApp.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {

        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ApplicationUserId { get; set; }
        //[Required]
        public string UctNumber { get; set; }
        //[Required]
        public string Permissions { get; set; }
        public bool Enabled { get; set; }
        


        public ApplicationUser()
        {
            this.SecurityStamp = Guid.NewGuid().ToString();
            this.Permissions = "Mentee";
        }

        public ApplicationUser(string userID, string permissions, string email, string username, string password)
        {
            
            this.UctNumber = userID;
            this.Enabled = true;
            this.Permissions = permissions;
            this.Email = email;
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            var hashed = passwordHasher.HashPassword(this, password);
            this.PasswordHash = hashed;
            this.UserName = username;

            this.SecurityStamp = Guid.NewGuid().ToString();

        }

        
    }
}