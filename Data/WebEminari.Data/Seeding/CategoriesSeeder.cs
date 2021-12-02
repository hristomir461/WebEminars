using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WebEminari.Data.Models;

namespace WebEminari.Data.Seeding
{
    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            await dbContext.Categories.AddAsync(new Category()
            {
                Name = "Software",
            });

            await dbContext.Categories.AddAsync(new Category()
            {
                Name = "Hardware",
            });

            await dbContext.Categories.AddAsync(new Category()
            {
                Name = "Business",
            });

            await dbContext.SaveChangesAsync();
        }
    }
}
