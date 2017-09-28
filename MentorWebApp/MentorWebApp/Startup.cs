using MentorWebApp.Data;
using MentorWebApp.Models;
using MentorWebApp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace MentorWebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddRoleManager<RoleManager<IdentityRole>>();


            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
<<<<<<< HEAD
            
=======
            services.AddAuthorization(options =>
            {
                options.AddPolicy("MustBeAdmin",
                    policy => policy.RequireRole("Admin"));
                options.AddPolicy("Mentee",
                    policy => policy.RequireRole("Mentee"));
                options.AddPolicy("Mentor",
                    policy => policy.RequireRole("Mentor"));
            });
>>>>>>> c2ae4fcc401fe7fb1fe952da4fe5a867b168edbb

            
            
            services.AddMvc();
            


            //
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
              
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            //app.UseMiddleware<UserManager<ApplicationUser>>();
            //app.UseMiddleware<RoleManager<IdentityRole>>();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });

            RolesData.SeedRoles(app).Wait();
            ApplicationDbContextSeedData.Seed(app);
            
        }
    }
}