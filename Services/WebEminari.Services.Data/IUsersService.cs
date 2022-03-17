using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WebEminari.Data.Models;

namespace WebEminari.Services.Data
{
    public interface IUsersService
    {
        Task<ApplicationUser> GetByIdAsync(string userId);
        int GetCount();
    }
}
