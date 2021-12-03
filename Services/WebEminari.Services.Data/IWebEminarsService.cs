using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using WebEminari.Data.Models;
using WebEminari.Web.ViewModels.WebEminars;

namespace WebEminari.Services.Data
{
    public interface IWebEminarsService
    {
        Task CreateAsync(WebEminarViewModel input, string userId, string ImageName);

        T GetById<T>(int id);

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage, string searchString, int categoryId);

        IEnumerable<T> GetRandom<T>(int count);

        Task UpdateAsync(int id, WebEminarViewModel input, string ImageName);

        int GetCount();

        Task DeleteAsync(int id);
        Task BookEvent(int eventId, ApplicationUser user);
    }
}
