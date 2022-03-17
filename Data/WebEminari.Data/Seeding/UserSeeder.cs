using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using WebEminari.Data.Models;

namespace WebEminari.Data.Seeding
{
    public class UserSeeder : ISeeder
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserSeeder(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var user = new ApplicationUser
            {
                UserName = "Email@email.com",
                NormalizedUserName = "email@email.com",
                Email = "Email@email.com",
                NormalizedEmail = "email@email.com",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var roleStore = new RoleStore<ApplicationRole>(dbContext);
            
            if (!dbContext.Roles.Any(r => r.Name == "admin"))
            {
                await roleStore.CreateAsync(new ApplicationRole { Name = "admin", NormalizedName = "admin" });
            }

            if (!dbContext.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(user, "password");
                user.PasswordHash = hashed;
                var userStore = new UserStore<ApplicationUser>(dbContext);
                await userStore.CreateAsync(user);
                await userStore.AddToRoleAsync(user, "admin");
            }

            await dbContext.SaveChangesAsync();
        }
    }
    }