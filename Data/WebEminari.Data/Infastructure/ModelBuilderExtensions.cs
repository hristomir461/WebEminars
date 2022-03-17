using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using WebEminari.Data.Models;

namespace WebEminari.Data.Infastructure
{
    public static class ModelBuilderExtensions
    {
        public static void ConfigureRelations(this ModelBuilder modelBuilder)
        {
            modelBuilder
           .Entity<Organization>()
           .HasOne(x => x.Creator)
           .WithMany(x => x.CreatedOrganizations)
           .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<ApplicationUser>()
                .HasMany(x => x.CreatedOrganizations)
                .WithOne(x => x.Creator)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<Organization>()
                .HasMany(x => x.Users)
                .WithMany(x => x.Organizations)
                .UsingEntity<Dictionary<string, object>>(
                    "OrganizationUser",
                    b => b.HasOne<ApplicationUser>().WithMany().OnDelete(DeleteBehavior.Cascade),
                    b => b.HasOne<Organization>().WithMany().OnDelete(DeleteBehavior.Cascade));

            modelBuilder.Entity<ChatApplicationUser>()
              .HasKey(x => new { x.ChatId, x.UserId });

            modelBuilder.Entity<Chat>().HasData(
               new Chat { Id = 1, Name = "Main", CreatedOn = DateTime.UtcNow });

            modelBuilder.Entity<ChatApplicationUser>().HasData(
                new ChatApplicationUser { UserId = "ec6406bf-06bd-414a-8dd2-c3418336f631", ChatId = 1 });
        }
    }
}