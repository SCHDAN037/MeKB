﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MentorWebApp.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
        }

        public ApplicationUser(string userID, string permissions, string email, string username, string password)
        {
            UctNumber = userID;
            Enabled = true;
            Permissions = permissions;
            Email = email;
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            var hashed = passwordHasher.HashPassword(this, password);
            PasswordHash = hashed;
            UserName = username;
        }
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public string ApplicationUserId { get; set; }

        //[Required]
        public string UctNumber { get; set; }

        //[Required]
        public string Permissions { get; set; }

        public bool Enabled { get; set; }


        public async Task<bool> ChangeRoleAsync(string role, string old, UserManager<ApplicationUser> userManager)
        {
            //var result = await userManager.RemoveFromRolesAsync(this, new List<string>()
            //{
            //    { "Admin"},
            //    { "Mentor" },
            //    { "Mentee" }
            //});

            var result = await userManager.RemoveFromRoleAsync(this, old);
            if (!result.Succeeded) return false;

            var addRes = await userManager.AddToRoleAsync(this, role);
            if (!addRes.Succeeded) return false;

            var update = await userManager.UpdateAsync(this);
            return update.Succeeded;


            //var currentRoles = userManager.GetRolesAsync(this);
            //currentRoles.Wait();
            //if (currentRoles.Result.Contains(role)) return false;
            //var result = userManager.AddToRoleAsync(this, role);
            //result.Wait();
            //if (result.IsCompletedSuccessfully)
            //{
            //    currentRoles = userManager.GetRolesAsync(this);
            //    currentRoles.Wait();
            //    foreach (var currentRole in currentRoles.Result)
            //    {
            //        if (!currentRole.Equals(role))
            //        {
            //            var res = userManager.RemoveFromRoleAsync(this, currentRole);
            //            res.Wait();


            //        }
            //    }
            //}

            //var update = userManager.UpdateAsync(this);
            //update.Wait();
            //var upres = update.Result;
            //currentRoles = userManager.GetRolesAsync(this);
            //currentRoles.Wait();
            //var debug = currentRoles.Result;
            //return true;
        }
    }
}