namespace WebEminari.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using WebEminari.Common;
    using WebEminari.Data.Models;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    internal class RolesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            //await SeedRoleAsync(roleManager, GlobalConstants.AdministratorRoleName);
            //await SeedAdmin(userManager, GlobalConstants.AdministratorRoleName); 
        }

        private static async Task SeedRoleAsync(RoleManager<ApplicationRole> roleManager, string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                var result = await roleManager.CreateAsync(new ApplicationRole(roleName));
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
        private static async Task SeedAdmin(UserManager<ApplicationUser> userManager, string roleName)
        {
            var user = new ApplicationUser
            {
                Id = "ec6406bf-06bd-414a-8dd2-c3418336f631",
                UserName = "Admin@email.com",
                NormalizedUserName = "admin@email.com",
                Email = "Admin@email.com",
                NormalizedEmail = "admin@email.com",
                EmailConfirmed = true,
                FirstName = "Admin",
                LastName = "Adminov",
                ImagePath = "274230082_1054205055138382_3138966343028686373_n-modified.png",
            };

            await userManager.CreateAsync(user, "123456");
            await userManager.AddToRoleAsync(user, roleName);


        }
    }
}
