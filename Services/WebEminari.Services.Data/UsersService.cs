using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using WebEminari.Data.Common.Repositories;
using WebEminari.Data.Models;

namespace WebEminari.Services.Data
{
    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> appUserRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public UsersService(
            IDeletableEntityRepository<ApplicationUser> appUserRepository,
            UserManager<ApplicationUser> userManager)
        {
            this.appUserRepository = appUserRepository;
            this.userManager = userManager;
        }

        public async Task<ApplicationUser> GetByIdAsync(string userId)
          => await this.appUserRepository
              .All()
              .FirstOrDefaultAsync(x => x.Id == userId);
        public int GetCount()
            => this.appUserRepository.All().Count();
    }
}
