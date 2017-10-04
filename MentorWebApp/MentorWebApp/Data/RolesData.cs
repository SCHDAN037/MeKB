using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace MentorWebApp.Data
{
    public static class RolesData
    {
        public static async Task SeedRoles(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                //var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();


                //Create All User Roles here!

                if (!await roleManager.RoleExistsAsync("Admin"))
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                if (!await roleManager.RoleExistsAsync("Mentor"))
                    await roleManager.CreateAsync(new IdentityRole("Mentor"));
                if (!await roleManager.RoleExistsAsync("Mentee"))
                    await roleManager.CreateAsync(new IdentityRole("Mentee"));
            }
        }
    }
}